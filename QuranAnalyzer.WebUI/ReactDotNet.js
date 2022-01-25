
(function (global, React)
{
    var createElement = React.createElement;

    function OnReady(callback)
    {
        document.addEventListener('DOMContentLoaded', callback);
    }

    function IterateObject(obj, fn)
    {
        for (var key in obj)
        {
            if (obj.hasOwnProperty(key))
            {
                fn(key, obj[key]);
            }
        }
    }

    function GetValueInPath(obj, steps)
    {
        var len = steps.length;

        for (var i = 0; i < len; i++)
        {
            if (obj == null)
            {
                throw 'Path is not read. Path:' + steps.join('.');
            }

            obj = obj[steps[i]];
        }

        return obj;
    }

    function SetValueInPath(obj, steps, value)
    {
        if (obj == null)
        {
            throw Error('SetValueInPath->' + value);
        }

        var len = steps.length;

        for (var i = 0; i < len; i++)
        {
            var step = steps[i];

            if (obj[step] == null)
            {
                obj[step] = {};
            }

            if (i === len - 1)
            {
                obj[step] = value;
            }
            else
            {
                obj = obj[step];
            }
        }
    }
    

    function Clone(obj)
    {
        return JSON.parse(JSON.stringify(obj));
    }

    function ConvertToReactElement(jsonNode, component)
    {
        // is ReactDotNet component
        if (jsonNode.fullName)
        {
            var cmp = DefineComponent(jsonNode);

            return createElement(cmp);
        }

        var i;

        var reactAttributes = jsonNode.reactAttributes;

        var reactAttributesLength = 0;

        var props = null;

        if (reactAttributes)
        {
            reactAttributesLength = reactAttributes.length;
        }

        if (reactAttributesLength > 0)
        {
            props = {};
        }

        var constructorFunction = jsonNode.tagName;
        if (jsonNode.jsLocation)
        {
            constructorFunction = GetValueInPath(window, jsonNode.jsLocation);
        }

        var children = jsonNode.children;

        var childrenLength = 0;
        if (children)
        {
            childrenLength = children.length;
        }

        // collect props

        function tryProcessAsEventHandler(propName)
        {
            var value = jsonNode[propName];
            if (value.$isRemoteMethod === true)
            {
                var remoteMethodName = value.remoteMethodName;

                props[propName] = function ()
                {
                    HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArguments: Array.prototype.slice.call(arguments) });
                }

                return true;
            }

            return false;
        }

        function tryProcessAsElement(propName)
        {
            var value = jsonNode[propName];
            if (value.$isElement === true)
            {
                props[propName] = ConvertToReactElement(value.element, component)

                return true;
            }

            return false;
        }

        function IfNull(value, defaultValue)
        {
            if (defaultValue === undefined)
            {
                return value;
            }

            if (value == null)
            {
                return defaultValue;
            }

            return value;
        }

        function tryProcessAsBinding(propName)
        {
            /*
            "valueBind": {
                "eventName": "onChange",
                "$isBinding": true,
                "jsValueAccess": ["e","target","value"],
                "sourcePath": ["innerA","innerB","text"],
                "targetProp": "value"
              }
             */

            var value = jsonNode[propName];

            if (value.$isBinding === true)
            {
                var targetProp    = value.targetProp;
                var eventName     = value.eventName;
                var sourcePath    = value.sourcePath;
                var jsValueAccess = value.jsValueAccess;
                var defaultValue  = value.defaultValue;

                props[targetProp] = IfNull(GetValueInPath(component.state.$state, sourcePath), defaultValue);
                props[eventName] = function (e)
                {
                    var state = Clone(component.state.$state);

                    SetValueInPath(state, sourcePath, IfNull(GetValueInPath({ e: e }, jsValueAccess)), defaultValue);

                    component.setState({ $state: state });
                }

                return true;
            }

            return false;
        }

        for (i = 0; i < reactAttributesLength; i++)
        {
            var propName = reactAttributes[i];

            var isProcessedAsEvent = tryProcessAsEventHandler(propName);
            if (isProcessedAsEvent)
            {
                continue;
            }

            var isProcessedAsBinding = tryProcessAsBinding(propName);
            if (isProcessedAsBinding)
            {
                continue;
            }

            var processedAsTemplate = tryProcessAsElement(propName);
            if (processedAsTemplate)
            {
                continue;
            }
            

            if (name.indexOf('bind$') === 0)
            {
                continue;
            }

            props[propName] = jsonNode[propName];
        }

        if (jsonNode.text != null)
        {
            return createElement(constructorFunction, props, jsonNode.text);
        }

        if (childrenLength > 0)
        {
            var newChildren = [];

            for (i = 0; i < childrenLength; i++)
            {
                newChildren.push(ConvertToReactElement(children[i], component));
            }

            return createElement(constructorFunction, props, newChildren);
        }

        return createElement(constructorFunction, props);
    }

    function NormalizeEventArguments(eventArguments)
    {
        function normalizeEventArgument(obj)
        {
            if (typeof obj === 'string')
            {
                return obj;
            }

            if (obj && obj._reactName === 'onClick')
            {
                return null;
            }

            var eventArgument = {};

            var hasArgument = false;
            IterateObject(obj, function (key, value)
            {
                if (key === 'originalEvent')
                {
                    return;
                }

                eventArgument[key] = value;

                hasArgument = true;
            });

            if (hasArgument === false)
            {
                return null;
            }

            return eventArgument;
        }

        var length = eventArguments.length;

        for (var i = 0; i < length; i++)
        {
            eventArguments[i] = normalizeEventArgument(eventArguments[i]);
        }

        if (length === 1)
        {
            if (eventArguments[0] === null)
            {
                return [];
            }
        }

        return eventArguments;
    }

    function HandleAction(data)
    {
        var remoteMethodName = data.remoteMethodName;
        var component = data.component;

        var request =
        {
            MethodName : "HandleComponentEvent",
            EventHandlerMethodName: remoteMethodName,
            FullName   : component.constructor.fullName,
            stateAsJson: JSON.stringify(component.state.$state)
        };

        var eventArguments = NormalizeEventArguments(data.eventArguments);

        if (eventArguments.length > 0)
        {
            var length = eventArguments.length;

            for (var i = 0; i < length; i++)
            {
                eventArguments[i] = JSON.stringify(eventArguments[i]);
            }

            request.eventArgumentsAsJsonArray = eventArguments;
        }

        function onSuccess(response)
        {
            if (response.errorMessage != null)
            {
                throw response.errorMessage;
            }

            var element = response.element;

            var clientTask = element.state.clientTask;
            if (clientTask)
            {
                element.state.clientTask = null;

                if (clientTask.comebackWithLastAction)
                {
                    var afterSetState = function ()
                    {
                        request.stateAsJson = JSON.stringify(component.state.$state);
                        global.ReactDotNet.SendRequest(request, onSuccess);
                    };

                    data.component.setState({
                        $rootNode: element.rootElement,
                        $state   : element.state
                    }, afterSetState);

                    return;
                }
            }

            data.component.setState({
                $rootNode: element.rootElement,
                $state   : element.state
            });
        }
        global.ReactDotNet.SendRequest(request, onSuccess);
    }

    function DefineComponent(componentDeclaration)
    {
        class NewComponent extends React.Component
        {
            constructor(props)
            {
                super(props);

                this.state =
                {
                    $rootNode: componentDeclaration.rootElement,
                    $state: componentDeclaration.state
                };

                this.fullName = componentDeclaration.fullName;
            }

            render()
            {
                return ConvertToReactElement(this.state.$rootNode, this);
            }
        }

        NewComponent.fullName = componentDeclaration.fullName;

        return NewComponent;
    }

    function FetchComponent(fullNameOfComponent, callback)
    {
        var request =
        {
            MethodName: "FetchComponent",
            FullName: fullNameOfComponent
        };

        function onSuccess(response)
        {
            if (response.errorMessage != null)
            {
                throw response.errorMessage;
            }

            var element = response.element;

            var component = DefineComponent(element);

            callback(React.createElement(component));
        }
        global.ReactDotNet.SendRequest(request, onSuccess);
    }


    global.ReactDotNet =
    {
        OnReady: OnReady,
        DefineComponent: DefineComponent,
        FetchComponent: FetchComponent
    };

})(window,React);



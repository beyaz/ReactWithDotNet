
(function (global, React)
{
    var createElement = React.createElement;

    function OnReady(callback)
    {
        document.addEventListener('DOMContentLoaded', callback);
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

        var constructorFunction = jsonNode.tag;
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

                props[propName] = function (e)
                {
                    HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArgument: e });
                }

                return true;
            }

            return false;
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
                var targetProp = value.targetProp;
                var eventName = value.eventName;
                var sourcePath = value.sourcePath;
                var jsValueAccess = value.jsValueAccess;

                props[targetProp] = GetValueInPath(component.state.$state, sourcePath);
                props[eventName] = function (e)
                {
                    var state = Clone(component.state.$state);

                    SetValueInPath(state, sourcePath, GetValueInPath({e: e}, jsValueAccess));

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

    function HandleAction(data)
    {
        var remoteMethodName = data.remoteMethodName;
        var component = data.component;
        var eventArgument = data.eventArgument;


        var request =
        {
            MethodName: "HandleComponentEvent",
            EventHandlerMethodName: remoteMethodName,
            FullName: component.constructor.fullName,
            stateAsJson: JSON.stringify(component.state.$state)
        };

        function onSuccess(response)
        {
            data.component.setState({
                $rootNode: response.rootElement,
                $state: response.state
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
            var component = DefineComponent(response);

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



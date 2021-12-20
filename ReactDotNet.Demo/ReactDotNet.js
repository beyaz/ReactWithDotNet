
(function (global, React)
{
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
        
        var createElement = React.createElement;

        if (jsonNode.fullName)
        {
            var cmp = DefineComponent(jsonNode);

            return createElement(cmp, jsonNode.props);
        }

        var constructorFunction = jsonNode.tag;

        var children = jsonNode.children || null;

        var childrenLength = 0;
        if (children)
        {
            childrenLength = children.length;
        }



        var props = jsonNode.props || {};

        var propNames = Object.keys(props);
        var propNamesLength = propNames.length;

        if (jsonNode.path)
        {
            constructorFunction = GetValueInPath(window, jsonNode.path);
        }

        // visit props
        var processedProps = {};

        function tryProcessAsEventHandler(name)
        {
            var value = props[name];

            if (typeof value === 'string')
            {
                var prefix = "$remote$";

                var index = value.indexOf(prefix);
                if (index === 0)
                {
                    var remoteMethodName = value.substr(prefix.length);

                    processedProps[name] = function (e)
                    {
                        HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArgument: e });
                    }

                    return true;
                }
            }

            return false;
        }

        function tryProcessAsBinding(name)
        {
            // sample: {  value:'abc', bind$value$onChange$e.target.value:'Prop1.a'  }

            var bindingSourcePath = null;
            var bindingTargetPath = null;
            var onChangeEventName = null;

            var bindAttributePrefix = 'bind$' + name + '$';
            for (var i = 0; i < propNamesLength; i++)
            {
                if (propNames[i].indexOf(bindAttributePrefix) === 0)
                {
                    var arr = propNames[i].split('$');

                    onChangeEventName = arr[2];
                    bindingTargetPath = arr[3].substr(2).split('.');
                    bindingSourcePath = props[propNames[i]].split('.');
                    break;
                }
            }

            if (bindingSourcePath)
            {
                processedProps[name] = GetValueInPath(component.state.$state, bindingSourcePath);
                processedProps[onChangeEventName] = function (e)
                {
                    var state = Clone(component.state.$state);

                    SetValueInPath(state, bindingSourcePath, GetValueInPath(e, bindingTargetPath));

                    component.setState({ $state: state });
                }

                return true;
            }

            return false;
        }

        // look events
        for (let name in props)
        {
            var isProcessedAsEvent = tryProcessAsEventHandler(name);
            if (isProcessedAsEvent)
            {
                continue;
            }

            var isProcessedAsBinding = tryProcessAsBinding(name);
            if (isProcessedAsBinding)
            {
                continue;
            }

            if (name.indexOf('bind$') === 0)
            {
                continue;
            }


            processedProps[name] = props[name];
        }
        props = processedProps;

        if (jsonNode.text != null)
        {
            return createElement(constructorFunction, props, jsonNode.text);
        }

        if (childrenLength > 0)
        {
            for (var i = 0; i < childrenLength; i++)
            {
                children[i] = ConvertToReactElement(children[i], component);
            }

            return createElement(constructorFunction, props, children);
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



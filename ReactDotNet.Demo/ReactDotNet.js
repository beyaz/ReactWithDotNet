
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

    function ConvertToReactElement(jsonNode, component)
    {
        
        var createElement = React.createElement;

        if (jsonNode.fullName)
        {
            var cmp = DefineComponent(jsonNode);

            return createElement(cmp);
        }

        var constructorFunction = jsonNode.tag;
        var children = jsonNode.children || null;
        var props = jsonNode.props || {};

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

        // look events
        for (let name in props)
        {
            var isProcessedAsEvent = tryProcessAsEventHandler(name);
            if (isProcessedAsEvent)
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

        if (children)
        {
            var len = children.length;
            for (var i = 0; i < len; i++)
            {
                children[i] = ConvertToReactElement(children[i], component);
            }
        }

      

        return createElement(constructorFunction, props, children);
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



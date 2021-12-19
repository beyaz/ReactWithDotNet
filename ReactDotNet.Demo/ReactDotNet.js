
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

        var constructorFunction = jsonNode.tag;
        var children = jsonNode.children || null;
        var props = jsonNode.props || {};

        if (jsonNode.path)
        {
            constructorFunction = GetValueInPath(window, jsonNode.path);
        }

        // visit props
        var processedProps = {};

        // look events
        for (let name in props)
        {
            if (name[0] === '$')
            {
                continue;
            }

            var isRemoteEvent = props['$' + name + 'IsRemote'] === true;
            if (isRemoteEvent)
            {
                var remoteMethodName = props[name];

                processedProps[name] = function (e)
                {
                    HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArgument: e });
                }

                continue;
            }

            processedProps[name] = props[name];
        }
        props = processedProps;

        if (children == null)
        {
            if (constructorFunction)
            {
                if (jsonNode.text != null)
                {
                    return createElement(constructorFunction, props, jsonNode.text);
                }
            }
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
        setTimeout(function()
            {
                data.component.setState({
                    $rootNode:
                    {
                        tag: 'div',
                        props: { style: { border: '1px solid blue' } },
                        children:
                        [
                            {
                                path: ['primereact', 'Button'],
                                props:
                                {
                                    key: '0',
                                    label: "aBc",
                                    onClick: 'b',
                                    $onClickIsRemote: true,
                                    style: { marginRight: '40px' }
                                }
                            },
                            {
                                path: ['primereact', 'Button'],
                                props:
                                {
                                    label: 'aBc',
                                    onClick: 'onBClicked',
                                    $onClickIsRemote: true,
                                    key: '1'
                                }
                            },
                                { tag: 'div', text: 'Aloha', props: { key: '2'} }
                        ]
                    }

                });
            },
            20);
    }

    function DefineComponent(componentDecleration)
    {
        class NewComponent extends React.Component
        {
            constructor(props)
            {
                super(props);

                this.state =
                {
                    $rootNode: componentDecleration.rootNode
                };

                this.fullName = componentDecleration.fullName;
            }

            render()
            {
                return ConvertToReactElement(this.state.$rootNode, this);
            }
        }

        NewComponent.fullName = componentDecleration.fullName;

        return NewComponent;
    }

    function FetchComponent(fullNameOfComponent, callback)
    {
        var request =
        {
            "method": "FetchComponent",
            "fullName": fullNameOfComponent
        };

        function onSuccess(json)
        {
            var component = DefineComponent({
                fullName: fullNameOfComponent,
                rootNode: json
            });

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



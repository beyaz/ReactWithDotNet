using System;
using Bridge;
using Bridge.Html5;
using Newtonsoft.Json;

namespace ReactDotNet
{
    [External]
    [ObjectLiteral]
    public class ReactElement
    {
        [Template("{0}.ToReactElement()")]
        public static implicit operator ReactElement(Element element)
        {
            return element.ToReactElement();
        }
    }

    [External]
    [Name("React.Component")]
    public class ReactComponentBase
    {
    }

    public class ReactComponent<TProps, TState> : ReactComponentBase, IElement where TState : new()
    {
        // ReSharper disable once UnassignedReadonlyField
        protected internal readonly TProps props;

        protected internal readonly TState state;

        public ReactComponent()
        {
            state = new TState();
        }

        public int? gravity { get; set; }

        [React]
        public string key { get; set; }

        [React]
        public CSSStyleDeclaration style { get; } = ObjectLiteral.Create<CSSStyleDeclaration>();

        public virtual ReactElement ToReactElement()
        {
            return React.createElement(GetType(), this.CollectReactAttributedProperties());
        }

        protected void SetState(Action<TState> modifyStateAction)
        {
            var clonedState = JsonConvert.DeserializeObject<TState>(JsonConvert.SerializeObject(state));

            modifyStateAction(clonedState);

            setState(clonedState);
        }

        #pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it
        extern void setState(TState newState);
        #pragma warning restore CS0626 // Method, operator, or accessor is marked external and has no attributes on it

        public virtual ReactElement render()
        {
            return null;
        }
    }
}
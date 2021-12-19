using System;


namespace ReactDotNet
{
    public interface IReactComponent
    {
        ReactElement ToReactElement();
    }

    public interface IReactComponent<TState>
    {
    }

    public abstract class ReactComponent<TState> : IReactComponent<TState>, IElement, IReactComponent where TState : new()
    {
        protected internal TState state;

        protected ReactComponent()
        {
            state = new TState();
        }

        public int? gravity { get; set; }

        [React]
        public string key { get; set; }

        [React]
        public CSSStyleDeclaration style { get; } = new CSSStyleDeclaration();

        public ReactElement ToReactElement()
        {
            return new ReactElement
            {
                FullName    = GetType().GetFullName(),
                RootElement = render(),
                State       = state
            };
        }

        public abstract ReactElement render();
    }
}
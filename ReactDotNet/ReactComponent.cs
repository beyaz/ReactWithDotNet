using System;


namespace ReactDotNet
{
    public interface IReactComponent
    {
        ReactElement ToReactElement();
    }

    public abstract class ReactComponent<TState> :  IElement, IReactComponent where TState : new()
    {
        protected internal TState state;

        protected ReactComponent()
        {
            state = new TState();
        }

        public int? gravity { get; set; }

        

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
using System;


namespace ReactDotNet
{
    public interface IReactComponent
    {
        ReactElement ToReactElement();
    }

    public abstract class ReactComponent<TState> :  Element, IReactComponent where TState : new()
    {
        protected internal TState state;

        protected ReactComponent()
        {
            state = new TState();
        }

        

        
        

        public override ReactElement ToReactElement()
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
using System;


namespace ReactDotNet
{
    
    public interface IReactComponent<TProps,TState>
    {
    }

    public abstract class ReactComponent<TProps, TState> : IReactComponent<TProps, TState>, IElement where TState : new()
    {
        // ReSharper disable once UnassignedReadonlyField
        protected internal readonly TProps props;

        protected internal readonly TState state;

        protected ReactComponent()
        {
            state = new TState();
        }

        public int? gravity { get; set; }

        [React]
        public string key { get; set; }

        [React]
        public CSSStyleDeclaration style { get; } = new CSSStyleDeclaration();

        public ReactElement ToReactElement() => render();





        public abstract ReactElement render();
    }
}
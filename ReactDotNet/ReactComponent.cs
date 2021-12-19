using System;


namespace ReactDotNet
{
    public interface IReactComponent
    {
         ReactElement render();
    }

    public interface IReactComponent<TProps,TState>
    {
    }

    public abstract class ReactComponent<TProps, TState> : IReactComponent<TProps, TState>, IElement, IReactComponent where TState : new()
    {
        // ReSharper disable once UnassignedReadonlyField
        protected internal readonly TProps props;

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

        public ReactElement ToReactElement() => render();





        public abstract ReactElement render();
    }
}
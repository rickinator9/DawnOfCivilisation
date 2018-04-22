using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public class ViewMediator<TView, TViewImpl> : Mediator
        where TViewImpl : View, TView
    {
        protected TView View { get; private set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View = GetComponent<TViewImpl>();
        }
    }
}
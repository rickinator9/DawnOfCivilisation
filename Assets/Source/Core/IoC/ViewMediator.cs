using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public class ViewMediator<TView, TViewImpl> : Mediator
        where TView : IView
        where TViewImpl : View, TView
    {
        private TView _view;
        public TView View
        {
            get
            {
                if (_view == null) _view = GetComponent<TViewImpl>();

                return _view;
            }
        }
    }
}
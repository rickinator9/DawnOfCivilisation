using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public abstract class ChildCreatorView<TChildView, TChildMediator, TObject> : View where TChildView : View
                                                                                       where TChildMediator : Mediator
    {
        private struct UnassignedMediator
        {
            public TChildView View;
            public TObject Object;
        }

        public TChildView ChildPrefab;

        private IList<UnassignedMediator> _unassignedMediators = new List<UnassignedMediator>();

        public void CreateViewForObject(TObject obj)
        {
            var view = Instantiate(ChildPrefab);
            view.transform.SetParent(transform);

            _unassignedMediators.Add(new UnassignedMediator
            {
                View = view,
                Object = obj
            });
        }

        protected void Update()
        {
            for (var i = _unassignedMediators.Count - 1; i >= 0; i--)
            {
                var unassignedMediator = _unassignedMediators[i];
                var mediator = unassignedMediator.View.GetComponent<TChildMediator>();
                if (mediator != null)
                {
                    _unassignedMediators.RemoveAt(i);
                    OnMediatorCreated(mediator, unassignedMediator.Object);
                }
            }
        }

        protected abstract void OnMediatorCreated(TChildMediator mediator, TObject obj);
    }
}
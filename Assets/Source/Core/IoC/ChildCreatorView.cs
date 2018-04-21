using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;

namespace Assets.Source.Core.IoC
{
    public abstract class ChildMediator<TObject, TView, TViewImpl> : ViewMediator<TView, TViewImpl> 
        where TView : IView
        where TViewImpl : View, TView
    {
        public abstract void Initialise(TObject obj);
    }

    public abstract class ChildCreatorView<TChildView, TChildViewImpl, TChildMediator, TObject> : View 
        where TChildView : IView
        where TChildViewImpl : View, TChildView
        where TChildMediator : ChildMediator<TObject, TChildView, TChildViewImpl>
    {
        private struct UnassignedMediator
        {
            public TChildViewImpl View;
            public TObject Object;
        }

        public TChildViewImpl ChildPrefab;

        private Queue<TObject> _objectCreationQueue = new Queue<TObject>();  
        private IList<UnassignedMediator> _unassignedMediators = new List<UnassignedMediator>();

        public void CreateViewForObject(TObject obj)
        {
            _objectCreationQueue.Enqueue(obj);
        }

        protected void Update()
        {
            while (_objectCreationQueue.Count != 0)
            {
                var obj = _objectCreationQueue.Dequeue();

                var view = Instantiate(ChildPrefab);
                view.transform.SetParent(transform);

                _unassignedMediators.Add(new UnassignedMediator
                {
                    View = view,
                    Object = obj
                });
            }

            for (var i = _unassignedMediators.Count - 1; i >= 0; i--)
            {
                var unassignedMediator = _unassignedMediators[i];
                var mediator = unassignedMediator.View.GetComponent<TChildMediator>();
                if (mediator != null)
                {
                    _unassignedMediators.RemoveAt(i);
                    mediator.Initialise(unassignedMediator.Object);
                }
            }
        }
    }
}
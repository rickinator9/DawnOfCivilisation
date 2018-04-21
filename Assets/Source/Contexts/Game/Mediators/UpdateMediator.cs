using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class UpdateSignal : Signal
    {
    }

    public class UpdateMediator : ViewMediator<UpdateView, UpdateView>
    {
        [Inject]
        public UpdateSignal UpdateDispatcher { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.OnUpdate.AddListener(OnUpdate);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.OnUpdate.RemoveListener(OnUpdate);
        }

        private void OnUpdate()
        {
            UpdateDispatcher.Dispatch();
        }
    }
}
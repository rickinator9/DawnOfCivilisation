using System.Diagnostics;
using Assets.Source.Contexts.Game.Commands.Input;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using Debug = UnityEngine.Debug;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class InputMediator : ViewMediator<InputView, InputView>
    {
        [Inject]
        public LeftMouseClickSignal LeftMouseClickDispatcher { get; set; }

        [Inject]
        public RightMouseClickSignal RightMouseClickDispatcher { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.LeftMouseClickSignal.AddListener(OnLeftMouseClick);
            View.RightMouseClickSignal.AddListener(OnRightMouseClick);
        }

        private void OnLeftMouseClick()
        {
            LeftMouseClickDispatcher.Dispatch();
        }

        private void OnRightMouseClick()
        {
            RightMouseClickDispatcher.Dispatch();
        }
    }
}
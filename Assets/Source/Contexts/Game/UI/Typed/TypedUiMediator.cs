using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI.Typed
{
    public abstract class TypedUiMediator<TView, TObject> : ViewMediator<TView> where TView : TypedUiView<TObject>
    {
        [Inject]
        public ShowUiPanelSignal ShowUiPanelSignal { get; set; }

        [Inject]
        public HideAllUiPanelsSignal HideAllUiPanelsSignal { get; set; }

        protected abstract UiType UiType { get; }

        public override void OnRegister()
        {
            base.OnRegister();

            ShowUiPanelSignal.AddListener(ShowUiPanel);
            HideAllUiPanelsSignal.AddListener(HidePanel);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            ShowUiPanelSignal.RemoveListener(ShowUiPanel);
            HideAllUiPanelsSignal.RemoveListener(HidePanel);
        }

        private void ShowUiPanel(UiType uiType, object uiObject)
        {
            if (uiType == UiType)
            {
                View.ShowForObject((TObject)uiObject);
            }
        }

        private void HidePanel()
        {
            View.Hide();
        }
    }
}
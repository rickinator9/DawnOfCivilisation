using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.api;

namespace Assets.Source.Contexts.Game.UI.Typed
{
    public abstract class TypedUiMediator<TView, TViewImpl, TObject> : ViewMediator<TView, TViewImpl> 
        where TView : IDOCView
        where TViewImpl : TypedUiView<TObject>, TView
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
                ShowUiPanelForObject((TObject)uiObject);
            }
        }

        protected abstract void ShowUiPanelForObject(TObject obj);

        private void HidePanel()
        {
            View.Hide();
        }
    }
}
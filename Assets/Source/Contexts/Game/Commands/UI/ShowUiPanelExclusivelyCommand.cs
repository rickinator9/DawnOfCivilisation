using Assets.Source.Contexts.Game.UI;
using Assets.Source.Contexts.Game.UI.Typed;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.UI
{
    #region Signals
    /// <summary>
    /// UiType: The type of UI that needs to be shown.
    /// object: The object the UI needs to be shown for.
    /// </summary>
    public class ShowUiPanelExclusivelySignal : Signal<UiType, object>
    {
        
    }

    /// <summary>
    /// UiType: The type of UI that needs to be shown.
    /// object: The object the UI needs to be shown for.
    /// </summary>
    public class ShowUiPanelSignal : Signal<UiType, object>
    {

    }

    public class HideAllUiPanelsSignal : Signal
    {
        
    }
    #endregion

    public class ShowUiPanelExclusivelyCommand : Command
    {
        #region From signal
        [Inject]
        public UiType UiType { get; set; }

        [Inject]
        public object UiObject { get; set; }
        #endregion

        #region Dependencies

        #endregion

        #region Dispatchers
        [Inject]
        public ShowUiPanelSignal ShowUiPanelDispatcher { get; set; }

        [Inject]
        public HideAllUiPanelsSignal HideAllUiPanelsDispatcher { get; set; }
        #endregion

        public override void Execute()
        {
            HideAllUiPanelsDispatcher.Dispatch();
            ShowUiPanelDispatcher.Dispatch(UiType, UiObject);
        }
    }
}
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Map
{
    #region Signals
    /// <summary>
    /// MapMode: The MapMode that should be set.
    /// </summary>
    public class SetMapModeSignal : Signal<MapMode> { }

    /// <summary>
    /// IMapMode: The MapMode that was just set.
    /// </summary>
    public class OnSetMapModeSignal : Signal<IMapMode> { }
    #endregion

    public class SetMapModeCommand : Command
    {
        #region From signal
        [Inject]
        public MapMode MapModeType { get; set; }
        #endregion

        #region Dependencies

        #endregion

        #region Dispatchers
        [Inject]
        public OnSetMapModeSignal OnSetMapModeDispatcher { get; set; }
        #endregion

        public override void Execute()
        {
            var mapMode = injectionBinder.GetInstance<IMapMode>(MapModeType);

            OnSetMapModeDispatcher.Dispatch(mapMode);
        }
    }
}
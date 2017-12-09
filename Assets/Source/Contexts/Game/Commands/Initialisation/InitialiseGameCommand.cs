using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Initialisation
{
    #region Signals
    public class InitialiseGameSignal : Signal { }
    #endregion

    public class InitialiseGameCommand : Command
    {
        [Inject]
        public InitialiseDateManagerSignal InitialiseDateManagerDispatcher { get; set; }

        [Inject]
        public InitialiseHexMapSignal InitialiseHexMapDispatcher { get; set; }

        [Inject]
        public InitialiseLocalPlayerSignal InitialiseLocalPlayerDispatcher { get; set; }

        [Inject]
        public InitialiseAiPlayersSignal InitialiseAiPlayersDispatcher { get; set; }

        public override void Execute()
        {
            InitialiseDateManagerDispatcher.Dispatch();
            InitialiseHexMapDispatcher.Dispatch(new HexMapDimension
            {
                Width = 20,
                Height = 20
            });
            InitialiseLocalPlayerDispatcher.Dispatch();
            InitialiseAiPlayersDispatcher.Dispatch();
        }
    }
}
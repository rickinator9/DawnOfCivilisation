using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Player;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Army
{
#region Signals
    /// <summary>
    /// IHexTile: The Tile the army will be spawned at.
    /// </summary>
    public class CreatePlayerArmySignal : Signal<IHexTile> { }
#endregion

    public class CreatePlayerArmyCommand : Command
    {
        [Inject]
        public IHexTile HexTile { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        [Inject]
        public CreateArmySignal CreateArmyDispatcher { get; set; }

        public override void Execute()
        {
            CreateArmyDispatcher.Dispatch(HexTile, Players.LocalPlayer.Country);
        }
    }
}
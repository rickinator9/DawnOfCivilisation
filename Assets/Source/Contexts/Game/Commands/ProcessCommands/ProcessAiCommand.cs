using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Model.Player;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.ProcessCommands
{
#region Signals
    public class ProcessAiSignal : Signal { }
#endregion

    public class ProcessAiCommand : Command
    {
        [Inject]
        public IPlayers Players { get; set; }

        [Inject]
        public CreateArmySignal CreateArmyDispatcher { get; set; }

        public override void Execute()
        {
            foreach (var player in Players.All)
            {
                if (player.Type == PlayerType.Ai) ProcessAiPlayer(player);
            }
        }

        public void ProcessAiPlayer(IPlayer aiPlayer)
        {
            var country = aiPlayer.Country;
            if (country.Wars.Length > 0 && country.Armies.Length == 0)
            {
                CreateArmyDispatcher.Dispatch(country.Location, country);
            }
        }
    }
}
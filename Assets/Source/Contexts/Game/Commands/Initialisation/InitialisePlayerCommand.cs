using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Initialisation
{
    #region Signals
    public class InitialisePlayerSignal : Signal { }
    #endregion

    public class InitialisePlayerCommand : Command
    {
        [Inject]
        public ICountries Countries { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        public override void Execute()
        {
            var player = injectionBinder.GetInstance<ILocalPlayer>(CustomContextKeys.NewInstance);
            player.Country = Countries.All[Countries.All.Length - 1];
            player.Country.IsPlayerControlled = true;
            Players.LocalPlayer = player;
        }
    }
}
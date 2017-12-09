using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Initialisation
{
    #region Signals
    public class InitialiseAiPlayersSignal : Signal
    {
        
    }
    #endregion

    public class InitialiseAiPlayersCommand : Command
    {
        [Inject]
        public ICountries Countries { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        public override void Execute()
        {
            var countriesWithoutPlayers = Countries.AllWithoutPlayers;
            foreach (var country in countriesWithoutPlayers)
            {
                var aiPlayer = injectionBinder.GetInstance<IAiPlayer>(CustomContextKeys.NewInstance);
                aiPlayer.Country = country;

                country.Player = aiPlayer;
                Players.Add(aiPlayer);
            }
        }
    }
}
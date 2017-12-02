using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.War
{
    #region Signals
    /// <summary>
    /// ICountry: The country the player declares war on.
    /// </summary>
    public class PlayerWarDeclarationSignal : Signal<ICountry> { }
    #endregion

    public class PlayerWarDeclarationCommand : Command
    {
        [Inject]
        public ICountry AttackedCountry { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        [Inject]
        public WarDeclarationSignal WarDeclarationDispatcher { get; set; }

        public override void Execute()
        {
            var warParams = new WarDeclarationParams()
            {
                Attacker = Players.LocalPlayer.Country,
                Defender = AttackedCountry
            };

            WarDeclarationDispatcher.Dispatch(warParams);
        }
    }
}
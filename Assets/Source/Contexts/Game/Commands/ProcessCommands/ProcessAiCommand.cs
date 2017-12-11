using System.Linq;
using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.War;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

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
        public ICountries Countries { get; set; }

        [Inject]
        public CreateArmySignal CreateArmyDispatcher { get; set; }

        [Inject]
        public CreateMovementPathSignal CreateMovementPathDispatcher { get; set; }

        [Inject]
        public WarDeclarationSignal WarDeclarationDispatcher { get; set; }

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
            if (country.Wars.Length == 0) DeclareWarOnClosestCountry(country);
            else
            {
                if (country.Armies.Length == 0) CreateArmy(country);
                if (country.Armies.Any(army => !army.IsMoving)) MoveArmies(country);
            }
        }

        private void DeclareWarOnClosestCountry(ICountry country)
        {
            ICountry closestCountry = null;

            foreach (var otherCountry in Countries.All)
            {
                if(otherCountry == country) continue;

                var distance = Vector3.Distance(country.Location.Center, otherCountry.Location.Center);
                if (closestCountry == null ||
                    distance < Vector3.Distance(country.Location.Center, closestCountry.Location.Center))
                    closestCountry = otherCountry;
            }

            if (closestCountry == null) return;

            WarDeclarationDispatcher.Dispatch(new WarDeclarationParams {Attacker = country, Defender = closestCountry});
        }

        private void CreateArmy(ICountry country)
        {
            CreateArmyDispatcher.Dispatch(country.Location, country);
        }

        private void MoveArmies(ICountry country)
        {
            foreach (var army in country.Armies)
            {
                var war = country.Wars[0];
                var primaryAttacker = war.GetEnemiesOfCountry(country)[0];
                CreateMovementPathDispatcher.Dispatch(
                    army,
                    new MovementPathParams() { Start = army.Location, Destination = GetClosestUnoccupiedTile(army.Location, primaryAttacker.Territories) }
                );
            }
        }

        private IHexTile GetClosestUnoccupiedTile(IHexTile from, ILandTile[] tiles)
        {
            IHexTile closest = null;
            foreach (var hexTile in tiles)
            {
                if (hexTile.Controller != hexTile.Country) continue;

                var distance = Vector3.Distance(from.Center, hexTile.Center);
                if (closest == null || distance < Vector3.Distance(closest.Center, from.Center))
                    closest = hexTile;
            }

            return closest != null ? closest : from;
        }
    }
}
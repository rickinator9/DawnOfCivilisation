using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Army
{
    #region Signals
    /// <summary>
    /// IHexTile: The tile the army will be spawned on.
    /// </summary>
    public class CreateArmySignal : Signal<IHexTile>
    {
        
    }
    
    /// <summary>
    /// IArmy: The created army.
    /// </summary>
    public class OnCreateArmySignal : Signal<IArmy>
    {

    }
    #endregion

    public class CreateArmyCommand : Command
    {
        [Inject]
        public IHexTile Tile { get; set; }

        [Inject]
        public OnCreateArmySignal OnCreateArmyDispatcher { get; set; }

        [Inject(CustomContextKeys.NewInstance)]
        public IArmy NewArmy { get; set; }

        [Inject]
        public IArmies Armies { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        [Inject]
        public IMovables Movables { get; set; }

        public override void Execute()
        {
            if (Tile.TerrainType == HexTerrainType.Water) return; // Cannot create armies on water.

            var landTile = (ILandTile) Tile;

            NewArmy.Location = landTile;
            NewArmy.Country = Players.LocalPlayer.Country;
            Armies.AddArmy(NewArmy);
            Movables.Add(NewArmy);

            landTile.Country = NewArmy.Country;

            OnCreateArmyDispatcher.Dispatch(NewArmy);
        }
    }
}
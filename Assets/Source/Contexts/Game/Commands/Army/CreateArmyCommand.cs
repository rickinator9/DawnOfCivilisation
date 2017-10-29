using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
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

        public override void Execute()
        {
            NewArmy.Location = Tile;
            NewArmy.Country = Players.Instance.LocalPlayer.Country;
            Armies.AddArmy(NewArmy);

            Tile.Country = NewArmy.Country;

            OnCreateArmyDispatcher.Dispatch(NewArmy);
        }
    }
}
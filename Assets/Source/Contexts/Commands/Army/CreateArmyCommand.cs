using Assets.Source.Contexts.Model;
using Assets.Source.Contexts.Signals.Armies;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine.WSA;

namespace Assets.Source.Contexts.Commands.Army
{
    /// <summary>
    /// IHexTile: The tile the army will be spawned on.
    /// </summary>
    public class CreateArmySignal : Signal<IHexTile>
    {
        
    }

    public class CreateArmyCommand : Command
    {
        [Inject]
        public IHexTile Tile { get; set; }

        [Inject]
        public OnCreateArmySignal OnCreateArmyDispatcher { get; set; }

        //[Inject]
        //public IArmy NewArmy { get; set; }

        public override void Execute()
        {
            var army = Armies.Instance.CreateArmy();
            army.Location = Tile;
            army.Country = Players.Instance.LocalPlayer.Country;

            Tile.Country = army.Country;

            OnCreateArmyDispatcher.Dispatch(army);
        }
    }
}
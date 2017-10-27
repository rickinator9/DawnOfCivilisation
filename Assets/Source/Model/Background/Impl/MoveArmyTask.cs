using Assets.Source.Contexts.Game.Model;

namespace Assets.Source.Model.Background.Impl
{
    public class MoveArmyTask : IBackgroundTask
    {
        public IDate ExecutionDate { get; private set; }

        private IArmy Army { get; set; }
        private IHexTile Destination { get; set; }

        public MoveArmyTask(IDate date, IArmy army, IHexTile destinationTile)
        {
            ExecutionDate = date;
            Army = army;
            Destination = destinationTile;
        }

        public void Execute()
        {
            Army.Location = Destination;
            Destination.Country = Army.Country;
        }
    }
}
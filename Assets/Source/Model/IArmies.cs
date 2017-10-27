using Assets.Source.Contexts.Model;

namespace Assets.Source.Model
{
    public interface IArmies
    {
        IArmy[] AllArmies { get; }

        IArmy CreateArmy();
    }
}
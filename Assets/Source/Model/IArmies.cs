using System.Collections.Generic;

namespace Assets.Source.Model
{
    public interface IArmies
    {
        IArmy[] AllArmies { get; }
        
        IArmy[] ArmiesWithoutView { get; }

        IArmy CreateArmy();
    }
}
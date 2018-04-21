using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views.Army;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Views
{

    public class ArmiesView : ChildCreatorView<IArmyView, ArmyView, ArmyMediator, IArmy>
    {
    }
}
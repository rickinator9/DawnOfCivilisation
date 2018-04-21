using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Views
{
    public class CityWorldPanelsView : ChildCreatorView<CityWorldPanelView, CityWorldPanelView, CityWorldPanelMediator, ICity>
    {
    }
}
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarsPanelView : ChildCreatorView<WarPanelView, WarPanelView, WarPanelMediator, IWar>
    {
    }
}
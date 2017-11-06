using System.Collections.Generic;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.Views
{
    public class CitiesView : ChildCreatorView<CityView, CityMediator, ICity>
    {
        protected override void OnMediatorCreated(CityMediator mediator, ICity obj)
        {
            
        }
    }
}
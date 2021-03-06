﻿using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class CityMediator : ChildMediator<ICity, CityView, CityView>
    {
        public override void Initialise(ICity city)
        {
            View.transform.position += city.Location.Center;
        }
    }
}
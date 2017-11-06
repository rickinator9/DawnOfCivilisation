﻿using System.Collections.Generic;
using Assets.Source.Contexts.Game.Mediators;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.Views
{

    public class ArmiesView : ChildCreatorView<ArmyView, ArmyMediator, IArmy>
    {
        protected override void OnMediatorCreated(ArmyMediator mediator, IArmy obj)
        {
            mediator.Initialise(obj);
        }
    }
}
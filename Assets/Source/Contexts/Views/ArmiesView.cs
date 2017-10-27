﻿using System.Collections.Generic;
using Assets.Source.Contexts.Mediators;
using Assets.Source.Contexts.Model;
using Assets.Source.Model.Impl;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Views
{

    public class ArmiesView : View
    {
        private struct UnassignedMediator
        {
            public ArmyView View;
            public IArmy Army;
        }

        public ArmyView ArmyPrefab;

        private IList<UnassignedMediator> _unassignedArmies = new List<UnassignedMediator>(); 

        public void CreateViewForArmy(IArmy army)
        {
            var armyView = Instantiate(ArmyPrefab);
            armyView.transform.SetParent(transform);

            _unassignedArmies.Add(new UnassignedMediator
            {
                View = armyView,
                Army = army
            });
        }

        void Update()
        {
            for (var i = _unassignedArmies.Count - 1; i >= 0; i--)
            {
                var unassignedArmy = _unassignedArmies[i];
                var armyMediator = unassignedArmy.View.GetComponent<ArmyMediator>();
                if (armyMediator != null)
                {
                    _unassignedArmies.RemoveAt(i);
                    armyMediator.Initialise(unassignedArmy.Army);
                }
            }
        }
    }
}
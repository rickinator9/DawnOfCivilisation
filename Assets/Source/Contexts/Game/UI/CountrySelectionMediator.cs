﻿using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Core.IoC;
using Assets.Source.Model.Impl;
using Assets.Source.UI.Controllers;

namespace Assets.Source.Contexts.Game.UI
{
    public class CountrySelectionMediator : ViewMediator<CountrySelectionView>
    {
        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.OnViewPressedSignal.AddListener(OnViewPressed);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.OnViewPressedSignal.RemoveListener(OnViewPressed);
        }

        private void OnViewPressed()
        {
            ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.CountryPanel, Players.LocalPlayer.Country);
        }
    }
}
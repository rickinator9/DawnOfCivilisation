﻿using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Contexts.Game.UI;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
using Assets.Source.UI.Controllers;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands.Input
{
    public class LeftMouseClickSignal : Signal { }

    public class LeftMouseClickCommand : Command
    {
        
        #region From signal

        #endregion

        #region Dependencies
        [Inject(CustomContextKeys.CurrentInstance)]
        public IHexMap HexMap { get; set; }
        #endregion

        #region Dispatchers
        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }
        #endregion

        public override void Execute()
        {
            IHexTile tile;
            RaycastHit hit;

            var foundTile = FindTileWithRayCast(out tile, out hit);
            if (foundTile)
            {
                if (hit.transform.gameObject.tag.Equals("HexGrid"))
                {
                    ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.HexPanel, tile);
                }
                else if (hit.transform.gameObject.tag.Equals("Army"))
                {
                    var army = tile.Armies[0];
                    Players.Instance.LocalPlayer.SelectedObject = army;

                    ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.ArmyPanel, army);
                }
            }
        }

        private bool FindTileWithRayCast(out IHexTile tile, out RaycastHit hit)
        {
            tile = null;
            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var hitPosition = hit.point;
                var coords = HexCoordinates.FromPosition(hitPosition).ToOffsetCoordinates();

                tile = HexMap[coords.X, coords.Z];
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands.Input
{
    public class RightMouseClickSignal : Signal
    {
        
    }

    public class RightMouseClickCommand : Command
    {
        #region From signal

        #endregion

        #region Dependencies
        [Inject(CustomContextKeys.CurrentInstance)]
        public IHexMap HexMap { get; set; }
        #endregion

        #region Dispatchers

        #endregion

        public override void Execute()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                IHexTile tile;

                var foundTile = FindTileWithRayCast(out tile, out hit);
                if (foundTile && tile.TerrainType != HexTerrainType.Water)
                {
                    var selectedObject = Players.Instance.LocalPlayer.SelectedObject;
                    if (selectedObject != null) selectedObject.OnRightClickOnTile(tile);
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

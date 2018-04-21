using Assets.Source.Contexts.Game.Commands.Initialisation;
using Assets.Source.Contexts.Game.Commands.Map;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class HexGridMediator : ViewMediator<HexGridView, HexGridView>
    {
        [Inject]
        public OnInitialiseHexMapSignal OnInitialiseHexMapSignal { get; set; }

        [Inject]
        public OnSetMapModeSignal OnSetMapModeSignal { get; set; }

        private IHexMap HexMap { get; set; }

        private IMapMode MapMode { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            OnInitialiseHexMapSignal.AddListener(OnInitialiseHexMap);
            OnSetMapModeSignal.AddListener(OnSetMapMode);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            OnInitialiseHexMapSignal.RemoveListener(OnInitialiseHexMap);
            OnSetMapModeSignal.RemoveListener(OnSetMapMode);
        }

        private void OnInitialiseHexMap(IHexMap hexMap)
        {
            HexMap = hexMap;

            var signal = new Signal();
            signal.AddListener(OnRefresh);
            foreach (var tile in HexMap.AllTiles)
            {
                tile.RefreshHexGridViewSignals.Add(signal);
            }

            OnRefresh(); // We need to draw the map.
        }

        private void OnSetMapMode(IMapMode mapMode)
        {
            MapMode = mapMode;
            View.MapMode = mapMode;

            if (View.Tiles != null) OnRefresh();
        }

        private void OnRefresh()
        {
            View.NeedsRefresh = true;
            View.Tiles = HexMap.AllTiles;
        }
    }
}
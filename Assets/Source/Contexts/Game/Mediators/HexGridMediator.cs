using Assets.Source.Contexts.Game.Commands.Initialisation;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class HexGridMediator : ViewMediator<HexGridView>
    {
        [Inject]
        public OnInitialiseHexMapSignal OnInitialiseHexMapSignal { get; set; }

        private IHexMap HexMap { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            OnInitialiseHexMapSignal.AddListener(OnInitialiseHexMap);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            OnInitialiseHexMapSignal.RemoveListener(OnInitialiseHexMap);
        }

        private void OnInitialiseHexMap(IHexMap hexMap)
        {
            HexMap = hexMap;

            var signal = new Signal();
            signal.AddListener(OnRefresh);
            foreach (var tile in HexMap.AllTiles)
            {
                tile.RefreshSignals.Add(signal);
            }

            OnRefresh(); // We need to draw the map.
        }

        private void OnRefresh()
        {
            View.NeedsRefresh = true;
            View.Tiles = HexMap.AllTiles;
        }
    }
}
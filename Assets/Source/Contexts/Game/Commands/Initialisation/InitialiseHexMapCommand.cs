using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Signals.Initialisation;
using Assets.Source.Core.IoC;
using Assets.Source.Hex;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
using Assets.Source.Utils;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.Initialisation
{
    public struct HexMapDimension
    {
        public int Width, Height;
    }

    /// <summary>
    /// HexMapDimension: Contains the dimensions of the HexMap.
    /// </summary>
    public class InitialiseHexMapSignal : Signal<HexMapDimension>
    {
        
    }

    public class InitialiseHexMapCommand : Command
    {
        #region From signal
        [Inject]
        public HexMapDimension Dimension { get; set; }

        #endregion

        #region Dependencies
        [Inject(CustomContextKeys.NewInstance)]
        public IHexMap HexMap { get; set; }
        #endregion

        #region Dispatchers
        [Inject]
        public OnInitialiseHexMapSignal OnInitialiseHexMapDispatcher { get; set; }
        #endregion

        private int Width { get { return Dimension.Width; } }
        private int Height { get { return Dimension.Height; } }

        public override void Execute()
        {
            HexMap.Initialise(Width, Height);

            for (var z = 0; z < Height; z++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var tile = CreateTile(x, z);
                    HexMap[x, z] = tile;

                    SetAdjacenciesForTile(tile, x, z);
                }
            }

            injectionBinder.Bind<IHexMap>().ToName(CustomContextKeys.CurrentInstance).ToValue(HexMap);
            OnInitialiseHexMapDispatcher.Dispatch(HexMap);
        }

        private IHexTile CreateTile(int x, int z)
        {
            var terrain = EnumUtils.GetRandomValue<HexTerrainType>();
            var tile = new HexTile
            {
                TerrainType = terrain,
                Coordinates = HexCoordinates.FromOffsetCoordinates(x, z)
            };

            return tile;
        }

        private void SetAdjacenciesForTile(IHexTile tile, int x, int z)
        {
            if (x > 0)
            {
                var leftTile = HexMap[x - 1, z];
                AddAdjacency(leftTile, tile, HexDirection.East);
            }
            if (z > 0)
            {
                if (z.IsEven())
                {
                    var northWestTile = HexMap[x, z - 1];
                    AddAdjacency(northWestTile, tile, HexDirection.SouthEast);
                    if (x > 0)
                    {
                        var northEastTile = HexMap[x - 1, z - 1];
                        AddAdjacency(northEastTile, tile, HexDirection.SouthWest);
                    }
                }
                else
                {
                    var northEastTile = HexMap[x, z - 1];
                    AddAdjacency(northEastTile, tile, HexDirection.SouthWest);
                    if (x < Width - 1)
                    {
                        var northWestTile = HexMap[x + 1, z - 1];
                        AddAdjacency(northWestTile, tile, HexDirection.SouthEast);
                    }
                }
            }
        }

        private void AddAdjacency(IHexTile tile1, IHexTile tile2, HexDirection direction)
        {
            tile1.AddNeighbor(tile2, direction);
            tile2.AddNeighbor(tile1, direction.Opposite());
        }
    }
}
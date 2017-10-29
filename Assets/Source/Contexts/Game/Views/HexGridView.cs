using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Model;
using Assets.Source.Model.Impl;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.Views
{
    public class HexGridView : View
    {

        public HexMesh Mesh;

        public bool NeedsRefresh { get; set; }

        public IHexTile[] Tiles { get; set; }

        protected override void Start()
        {
            base.Start();

            var players = Players.Instance;
            var player = new LocalPlayer { Country = new Country("Sumeria") };
            players.LocalPlayer = player;
        }

        protected void Update()
        {
            if (NeedsRefresh)
            {
                Mesh.Triangulate(Tiles);
                NeedsRefresh = false;
            }
        }
    }
}
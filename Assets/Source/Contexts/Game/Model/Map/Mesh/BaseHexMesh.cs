using Assets.Source.Contexts.Game.Model.Map.MapMode;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map.Mesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class BaseHexMesh : MonoBehaviour
    {
        protected UnityEngine.Mesh Mesh { get; set; }

        protected MeshCollider Collider { get; set; }

        void Awake()
        {
            Mesh = GetComponent<MeshFilter>().mesh = new UnityEngine.Mesh();
            Collider = gameObject.AddComponent<MeshCollider>();

            Initialise();
        }

        protected abstract void Initialise();

        public void Triangulate(IHexTile[] tiles, IMapMode mapMode)
        {
            TriangulateTiles(tiles, mapMode);

            SetMeshProperties();

            Collider.sharedMesh = Mesh;

            ClearMeshProperties();
        }

        protected abstract void TriangulateTiles(IHexTile[] tiles, IMapMode mapMode);

        protected abstract void SetMeshProperties();

        protected abstract void ClearMeshProperties();
    }
}
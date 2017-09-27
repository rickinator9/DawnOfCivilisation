using UnityEngine;

namespace Assets.Source.Hex
{
    public class HexTile : MonoBehaviour
    {
        public int X, Z;

        public Vector3 Position
        {
            get { return transform.localPosition; }
            set { transform.localPosition = value; }
        }
    }
}

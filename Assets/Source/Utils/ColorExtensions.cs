using UnityEditor;
using UnityEngine;

namespace Assets.Source.Utils
{
    public static class ColorUtils
    {
        public static Color GetRandomColor()
        {
            var r = Random.value;
            var g = Random.value;
            var b = Random.value;

            return new Color(r, g, b);
        }

        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }
    }
}
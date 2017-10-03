using System;

namespace Assets.Source.Utils
{
    public static class EnumUtils
    {

        private static readonly Random Random = new Random();
        public static T GetRandomValue<T>() where T : IConvertible
        {
            var values = Enum.GetValues(typeof (T));

            return (T)values.GetValue(Random.Next(values.Length));
        }
    }
}
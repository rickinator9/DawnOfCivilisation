using UnityEngine;

namespace Assets.Source.Model
{
    public interface ICountry
    {
         string Name { get; set; }

         Color Color { get; }
    }
}
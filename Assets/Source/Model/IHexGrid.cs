using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Hex;

namespace Assets.Source.Model
{
    public interface IHexGrid
    {
        IList<IHexTile> FindPath(IHexTile start, IHexTile goal);
    }
}
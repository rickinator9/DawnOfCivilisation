using System.Collections.Generic;

namespace Assets.Source.Model
{
    public interface IHexGrid
    {
        IList<IHexTile> FindPath(IHexTile start, IHexTile goal);
    }
}
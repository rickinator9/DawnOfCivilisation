using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Map;

namespace Assets.Source.Contexts.Game.Model.Pathfinding
{
    public interface IPathfinding
    {
        IList<IHexTile> FindPath(IHexTile start, IHexTile goal);
    }
}
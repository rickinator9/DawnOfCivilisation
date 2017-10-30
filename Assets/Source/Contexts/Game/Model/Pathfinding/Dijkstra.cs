using System.Collections.Generic;
using System.Linq;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model.Pathfinding
{
    public class Dijkstra : IPathfinding
    {
        internal class Node
        {
            public IHexTile Tile { get; set; }

            public float Cost { get; set; }

            public Node Parent { get; set; }
        }

        public IList<IHexTile> FindPath(IHexTile start, IHexTile goal)
        {
            var unvisitedNodes = new HashSet<Node>();
            var visitedNodes = new HashSet<Node>();
            var nodeByTile = new Dictionary<IHexTile, Node>();

            var startNode = new Node() {Cost = 0, Tile = start};
            unvisitedNodes.Add(startNode);
            nodeByTile[start] = startNode;

            while (unvisitedNodes.Count > 0)
            {
                var closestNode = GetNodeWithSmallestCost(unvisitedNodes);
                unvisitedNodes.Remove(closestNode);
                visitedNodes.Add(closestNode);

                // Add all of the neighbours to the unvisited nodes if they have not been visited yet.
                foreach (var neighbor in closestNode.Tile.Neighbors)
                {
                    if (neighbor == null || neighbor.TerrainType == HexTerrainType.Water || nodeByTile.ContainsKey(neighbor)) continue; // This tile has already been reached by the algorithm.

                    var node = new Node()
                    {
                        Cost = closestNode.Cost + neighbor.TerrainType.GetCost(),
                        Parent = closestNode,
                        Tile = neighbor
                    };
                    unvisitedNodes.Add(node);
                    nodeByTile[neighbor] = node;
                }
            }

            // If goal could not be found, just return an empty list.
            if(!nodeByTile.ContainsKey(goal)) return new List<IHexTile>(0);

            // Make a path using the parents.
            var tiles = new Stack<IHexTile>();
            var goalNode = nodeByTile[goal];
            var pathNode = goalNode;
            while (pathNode.Parent != null)
            {
                tiles.Push(pathNode.Tile);
                pathNode = pathNode.Parent;
            }
            tiles.Push(pathNode.Tile);

            return tiles.ToList();
        }

        private Node GetNodeWithSmallestCost(HashSet<Node> nodes)
        {
            Node nodeWithLowestCost = null;
            foreach (var node in nodes)
            {
                if (nodeWithLowestCost == null || node.Cost < nodeWithLowestCost.Cost)
                {
                    nodeWithLowestCost = node;
                }
            }

            return nodeWithLowestCost;
        }
    }
}
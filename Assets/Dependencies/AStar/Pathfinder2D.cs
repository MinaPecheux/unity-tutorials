// Adapted from: https://github.com/pixelfac/2D-Astar-Pathfinding-in-Unity
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{

    public class Pathfinder2D
    {
        private Grid2D _grid;

        public Pathfinder2D(UnityEngine.Tilemaps.Tilemap tilemap)
        {
            _grid = tilemap.GetComponent<Grid2D>();
        }

        public List<Vector3> FindPath(Vector3 start, Vector3 target)
        {
            if (_grid == null)
            {
                Debug.LogWarning("Invalid grid for Pathfinder2D!");
                return null;
            }

            List<Vector3> path = new List<Vector3>();

            Node2D seekerNode = _grid.NodeFromWorldPoint(start);
            Node2D targetNode = _grid.NodeFromWorldPoint(target);

            List<Node2D> openSet = new List<Node2D>();
            HashSet<Node2D> closedSet = new HashSet<Node2D>();
            openSet.Add(seekerNode);

            while (openSet.Count > 0)
            {
                // find lowest fcost
                Node2D node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                    if (openSet[i].fCost <= node.fCost)
                        if (openSet[i].hCost < node.hCost)
                            node = openSet[i];

                openSet.Remove(node);
                closedSet.Add(node);

                // found target: retrace steps
                if (node == targetNode)
                {
                    Node2D currentNode = targetNode;
                    while (currentNode != seekerNode)
                    {
                        path.Add(currentNode.worldPosition);
                        currentNode = currentNode.parent;
                    }
                    path.Reverse();
                }

                // add neighbours to openSet
                foreach (Node2D neighbour in _grid.GetNeighbors(node))
                {
                    if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = node.gCost + _GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = _GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return path;
        }

        private int _GetDistance(Node2D a, Node2D b)
        {
            int distX = Mathf.Abs(a.gridX - b.gridX);
            int distY = Mathf.Abs(a.gridY - b.gridY);
            if (distX > distY)
                return 14 * distY + 10 * (distX - distY);
            return 14 * distX + 10 * (distY - distX);
        }
    }

}

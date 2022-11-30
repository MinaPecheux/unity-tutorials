// Adapted from: https://github.com/pixelfac/2D-Astar-Pathfinding-in-Unity
using UnityEngine;

namespace AStar
{

    public class Node2D
    {
        public int gCost, hCost;
        public Vector3 worldPosition;
        public int gridX, gridY;
        public Node2D parent;
        public bool isWalkable;

        public int fCost => gCost + hCost;
    }

}

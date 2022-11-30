// Adapted from: https://github.com/pixelfac/2D-Astar-Pathfinding-in-Unity
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AStar
{

    [RequireComponent(typeof(Tilemap))]
    public class Grid2D : MonoBehaviour
    {
        public Vector2 gridWorldSize;
        public Node2D[,] grid;

        private Tilemap _tilemap;
        private Vector3 _worldBottomLeft;
        private float _nodeRadius, _nodeDiameter;
        private int _gridSizeX, _gridSizeY;

        [ContextMenu("Setup")]
        void Awake()
        {
            _tilemap = GetComponent<Tilemap>();

            _nodeDiameter = _tilemap.transform.parent.GetComponent<Grid>().cellSize.x;
            _nodeRadius = _nodeDiameter / 2f;
            _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
            _SetupGrid();
        }

        private void _SetupGrid()
        {
            grid = new Node2D[_gridSizeX, _gridSizeY];
            _worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPosition = _worldBottomLeft
                        + Vector3.right * (x * _nodeDiameter + _nodeRadius)
                        + Vector3.up * (y * _nodeDiameter + _nodeRadius);
                    grid[x, y] = new Node2D()
                    {
                        gCost = 0, hCost = 0,
                        isWalkable = _tilemap.HasTile(_tilemap.WorldToCell(worldPosition)),
                        worldPosition = worldPosition,
                        gridX = x, gridY = y,
                    };
                }
            }
        }

        public List<Node2D> GetNeighbors(Node2D node)
        {
            List<Node2D> neighbors = new List<Node2D>();

            // top
            if (node.gridX >= 0 && node.gridX < _gridSizeX && node.gridY + 1 >= 0 && node.gridY + 1 < _gridSizeY)
                neighbors.Add(grid[node.gridX, node.gridY + 1]);

            // bottom
            if (node.gridX >= 0 && node.gridX < _gridSizeX && node.gridY - 1 >= 0 && node.gridY - 1 < _gridSizeY)
                neighbors.Add(grid[node.gridX, node.gridY - 1]);

            // right
            if (node.gridX + 1 >= 0 && node.gridX + 1 < _gridSizeX && node.gridY >= 0 && node.gridY < _gridSizeY)
                neighbors.Add(grid[node.gridX + 1, node.gridY]);

            // left
            if (node.gridX - 1 >= 0 && node.gridX - 1 < _gridSizeX && node.gridY >= 0 && node.gridY < _gridSizeY)
                neighbors.Add(grid[node.gridX - 1, node.gridY]);

            return neighbors;
        }

        public Node2D NodeFromWorldPoint(Vector3 worldPosition)
        {
            Vector3Int p = _tilemap.WorldToCell(worldPosition);
            int x = Mathf.RoundToInt(p.x + (_gridSizeX / 2));
            int y = Mathf.RoundToInt(p.y + (_gridSizeY / 2));
            return grid[x, y];
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

            if (grid != null)
            {
                foreach (Node2D n in grid)
                {
                    Gizmos.color = n.isWalkable ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * _nodeRadius);
                }
            }
        }
    }

}

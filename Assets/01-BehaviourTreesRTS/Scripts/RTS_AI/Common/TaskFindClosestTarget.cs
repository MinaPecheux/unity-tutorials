using UnityEngine;
using UnityEngine.Tilemaps;

using BehaviorTree;

public class TaskFindClosestTarget : Node
{
    private Transform _transform;
    private Tilemap _searchTilemap;
    private bool _searchingForResource;

    private int _maxGridSize;
    private Vector3 _gridTileAnchor;

    public TaskFindClosestTarget(Transform transform, Tilemap searchTilemap, bool searchingForResource)
    {
        _transform = transform;
        _searchTilemap = searchTilemap;
        _searchingForResource = searchingForResource;

        BoundsInt tilemapBounds = _searchTilemap.cellBounds;
        _maxGridSize = Mathf.Max(tilemapBounds.size.x, tilemapBounds.size.y);
        _gridTileAnchor = _searchTilemap.tileAnchor;
    }

    public override NodeState Evaluate()
    {
        Vector3Int p = Vector3Int.FloorToInt(_transform.position);
        for (int searchRadius = 0; searchRadius < _maxGridSize; searchRadius++)
        {
            for (int x = p.x - searchRadius; x <= p.x + searchRadius; x++)
            {
                for (int y = p.y - searchRadius; y <= p.y + searchRadius; y++)
                {
                    Vector3 worldPos = new Vector3(x, y, 0) + _gridTileAnchor;
                    Vector3Int cellPos = _searchTilemap.WorldToCell(worldPos);
                    if (_searchTilemap.HasTile(cellPos))
                    {
                        Root.SetData("target", worldPos);
                        Root.SetData("target_cell", cellPos);
                        Root.SetData("target_is_resource", _searchingForResource);
                        _state = NodeState.SUCCESS;
                        return _state;
                    }
                }
            }
        }

        _state = NodeState.FAILURE;
        return _state;
    }

}

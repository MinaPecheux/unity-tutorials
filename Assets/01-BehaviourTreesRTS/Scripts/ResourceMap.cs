using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ResourceTile
{
    public Sprite sprite;
    public int value;
}

public class ResourceItem
{
    public int maxHP;
    public int curHP;
}

[RequireComponent(typeof(Tilemap))]
public class ResourceMap : MonoBehaviour
{
    [SerializeField] private Sprite _deadResourceSprite;
    [SerializeField] private ResourceTile[] _tiles;

    private Tilemap _tilemap;
    private Tilemap _deadItemsTilemap;
    private Dictionary<(int, int), ResourceItem> _tileValues;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _deadItemsTilemap = transform.Find("DeadItems").GetComponent<Tilemap>();

        _tileValues = new Dictionary<(int, int), ResourceItem>();
        BoundsInt bounds = _tilemap.cellBounds;
        for (int x = bounds.position.x; x < bounds.position.x + bounds.size.x; x++)
        {
            for (int y = bounds.position.y; y < bounds.position.y + bounds.size.y; y++)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                if (_tilemap.HasTile(p))
                {
                    int v = _GetTileValue(p);
                    _tileValues[(x, y)] = new ResourceItem() { maxHP = v, curHP = v };
                }
            }
        }
    }

    [ContextMenu("Auto Fetch Tiles")]
    private void _AutoFetchTiles()
    {
        _tilemap = GetComponent<Tilemap>();

        int nSprites = _tilemap.GetUsedSpritesCount();
        Sprite[] tilemapSprites = new Sprite[nSprites];
        _tilemap.GetUsedSpritesNonAlloc(tilemapSprites);

        Dictionary<Sprite, int> previousValues = new Dictionary<Sprite, int>();
        foreach (ResourceTile t in _tiles)
            previousValues[t.sprite] = t.value;

        _tiles = new ResourceTile[nSprites];
        for (int i = 0; i < nSprites; i++)
        {
            int v = 0;
            if (previousValues.ContainsKey(tilemapSprites[i]))
                v = previousValues[tilemapSprites[i]];
            _tiles[i] = new ResourceTile() { sprite = tilemapSprites[i], value = v };
        }
    }

    private int _GetTileValue(Vector3Int p)
    {
        Sprite s = _tilemap.GetSprite(p);
        foreach (ResourceTile t in _tiles)
            if (t.sprite == s)
                return t.value;
        return 0;
    }

    public bool ConsumeTile(Vector3Int cellPos, int amount)
    {
        (int, int) p = (cellPos.x, cellPos.y);
        ResourceItem i = _tileValues[p];
        i.curHP -= amount;
        if (i.curHP <= 0)
        {
            Tile t = ScriptableObject.CreateInstance<Tile>();
            t.sprite = _deadResourceSprite;
            _deadItemsTilemap.SetTile(cellPos, t);
            _tilemap.SetTile(cellPos, null);
            _tileValues.Remove(p);
            return true;
        }
        return false;
    }

}

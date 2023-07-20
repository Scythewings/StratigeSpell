using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTiles overLayTilesPrefab;
    public GameObject overlayContainer;

    public Dictionary<Vector2Int, OverlayTiles> map;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        { 
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var tilesMap = GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTiles>();
        BoundsInt bounds = tilesMap.cellBounds;

        for (int z = bounds.max.z; z > bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var tilesLocation = new Vector3Int(x, y, z);
                    var tilesKey = new Vector2Int(x, y);

                    if (tilesMap.HasTile(tilesLocation) && !map.ContainsKey(tilesKey))
                    {
                        var overLayTiles = Instantiate(overLayTilesPrefab, overlayContainer.transform);
                        var cellWoldPosition = tilesMap.GetCellCenterWorld(tilesLocation);

                        overLayTiles.transform.position = new Vector3(cellWoldPosition.x, cellWoldPosition.y, cellWoldPosition.z + 1);
                        overLayTiles.GetComponent<SpriteRenderer>().sortingOrder = tilesMap.GetComponent<TilemapRenderer>().sortingOrder;
                        overLayTiles.gridLocation = tilesLocation;
                        map.Add(tilesKey, overLayTiles);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

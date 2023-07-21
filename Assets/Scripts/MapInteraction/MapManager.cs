using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public GameObject overLayTilesPrefab;
    public GameObject overlayContainer;

    public Dictionary<Vector2Int, OverlayTiles> map;

    private void Awake() //work
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
        var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
        map = new Dictionary<Vector2Int, OverlayTiles>();

        foreach (var tm in tileMaps)
        {
            BoundsInt bounds = tm.cellBounds;

            for (int z = bounds.max.z; z > bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        if (tm.HasTile(new Vector3Int(x, y, z)))
                        {
                            if (!map.ContainsKey(new Vector2Int(x, y)))
                            {
                                var overlayTile = Instantiate(overLayTilesPrefab, overlayContainer.transform);
                                var cellWorldPosition = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder;
                                overlayTile.gameObject.GetComponent<OverlayTiles>().gridLocation = new Vector3Int(x, y, z);

                                map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTiles>());
                            }
                        }
                    }
                }
            }
        }
    }
}

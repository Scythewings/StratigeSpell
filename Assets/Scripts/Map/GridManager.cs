using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tiles _tile;

    // Start is called before the first frame update
    void Start()
    {
        generateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void generateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                /*GenerateMap(x, y, 0, 1, 14, 18);
                GenerateMap(x, y, 2, 2, 11, 21)*/
                var _spawnTiles = Instantiate(_tile, new Vector3(x, y), Quaternion.identity);
                _spawnTiles.name = $"Tile {x} {y}";
            }
        }
    }

    void GenerateMap(int x, int y, int widthmax, int widthmin, int heightmx, int heightmin)
    {
        if ((x >= widthmax) && (x <= widthmin) && (y >= heightmx) && (y <= heightmin))
        {
            var _spawnTiles = Instantiate(_tile, new Vector3(x, y), Quaternion.identity);
            _spawnTiles.name = $"Tile {x} {y}";
        }
    }
}

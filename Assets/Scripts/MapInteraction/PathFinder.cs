using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTiles> FindPath(OverlayTiles start, OverlayTiles end)
    {
        List<OverlayTiles> openList = new List<OverlayTiles>();
        List<OverlayTiles> closedList = new List<OverlayTiles>();
        openList.Add(start);
        while (openList.Count > 0)
        {
            OverlayTiles currentOverTile = openList.OrderBy(x => x.F).First();
            openList.Remove(currentOverTile);
            closedList.Add(currentOverTile);
            if (currentOverTile == end)
            {
                //Finalize our Path
                return GetFinishList(start, end);

            }
            var neighbourTiles = GetNeighbourTiles(currentOverTile);
            foreach (var neighbour in neighbourTiles)
            {
                if (neighbour.isBlocked || closedList.Contains(neighbour) || Mathf.Abs(currentOverTile.gridLocation.z - neighbour.gridLocation.z) > 1)
                {
                    continue;
                }
                neighbour.G = GetManhattenDistance(start, neighbour);
                neighbour.H = GetManhattenDistance(end, neighbour);
                neighbour.previous = currentOverTile;
                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<OverlayTiles>();
    }
    private List<OverlayTiles> GetFinishList(OverlayTiles start, OverlayTiles end)
    {
        List<OverlayTiles> finishedList = new List<OverlayTiles>();
        OverlayTiles currentTile = end;
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }
        finishedList.Reverse();
        return finishedList;
    }

    private int GetManhattenDistance(OverlayTiles start, OverlayTiles neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x)+ Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    private List<OverlayTiles> GetNeighbourTiles(OverlayTiles currentOverTile)
    {
        var map = MapManager.Instance.map;
        List<OverlayTiles> neighbours = new List<OverlayTiles>();
        Vector2Int locationToCheck = new Vector2Int(currentOverTile.gridLocation.x, currentOverTile.gridLocation.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverTile.gridLocation.x, currentOverTile.gridLocation.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverTile.gridLocation.x+1, currentOverTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverTile.gridLocation.x-1, currentOverTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }
        return neighbours;
    }
}
 
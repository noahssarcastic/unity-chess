using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap
{
    // Member variables

    private float[,] tiles;

    // Properties

    public int Width { get; private set; }

    public int Height  { get; private set; }

    public float TileSize { get; private set; }

    // Constructors

    public TileMap(int x, int y, float size=1f)
    {
        Width = x;
        Height = y;
        TileSize = size;
        tiles = new float[x, y];
    }
    
    // Methods

    public float GetTile(int x, int y) {
        return tiles[x, y];
    }

    public void SetTile(int x, int y, float value) {
        tiles[x, y] = value;
    }
    
    public Vector3 GetWorldCoords(int x, int y) {
        return new Vector3(x, y) * TileSize;
    }

    public (int, int) GetGridCoords(Vector3 pos) {
        return (
            Mathf.FloorToInt(pos.x / TileSize), 
            Mathf.FloorToInt(pos.y / TileSize)
        );
    }
}

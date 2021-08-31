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

    public TileMap(int x, int y, float size)
    {
        Width = x;
        Height = y;
        TileSize = size;
        tiles = new float[x, y];
    }

    public TileMap(int x, int y, float size, int defaultValue)
    {
        Width = x;
        Height = y;
        TileSize = size;
        tiles = new float[x, y];
        for (int i = 0; i < Width; i++) {
            for (int j = 0; j < Height; j++) {
                tiles[i, j] = defaultValue;
            }
        }
    }
    
    // Methods

    public float GetTile(int x, int y) {
        return tiles[x, y];
    }

    public float GetTile(Coords<int> coords) {
        return tiles[coords.X, coords.Y];
    }

    public void SetTile(int x, int y, float value) {
        tiles[x, y] = value;
    }

    public void SetTile(Coords<int> coords, float value) {
        tiles[coords.X, coords.Y] = value;
    }

    public Vector3 GetWorldCoords(int x, int y) {
        return new Vector3(x, y) * TileSize;
    }
    
    public Vector3 GetWorldCoords(Coords<int> coords) {
        return new Vector3(coords.X, coords.Y) * TileSize;
    }

    public Coords<int> GetGridCoords(Vector3 position) {
        return new Coords<int>(
            Mathf.FloorToInt(position.x / TileSize), 
            Mathf.FloorToInt(position.y / TileSize)
        );
    }

    public bool InBounds(int x, int y) {
        if (x < 0) return false;
        if (x >= Width) return false;
        if (y < 0) return false;
        if (y >= Height) return false;
        return true;
    }

    public bool InBounds(Coords<int> coords) {
        if (coords.X < 0) return false;
        if (coords.X >= Width) return false;
        if (coords.Y < 0) return false;
        if (coords.Y >= Height) return false;
        return true;
    }
}

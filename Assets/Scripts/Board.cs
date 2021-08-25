using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private TileMap tileMap;
    private Tile[,] tiles;

    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1;
    [SerializeField] private GameObject tilePrefab;

    // Start is called before the first frame update
    void Start() {
        tileMap = new TileMap(width, height, tileSize);
        tiles = new Tile[width, height];

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Tile tile = CreateTile(
                    string.Format("Tile {0}, {1}", i, j),
                    new Vector3(i * tileSize, j * tileSize),
                    new Vector3(tileSize, tileSize)
                );
                tiles[i, j] = tile;

                // Render tile
                tile.SetText(tileMap.GetTile(i, j).ToString());
                if ((i + j) % 2 == 0) {
                    tile.SetColor(new Color(87/256f, 58/256f, 46/256f));
                } else {
                    tile.SetColor(new Color(252/256f, 204/256f, 116/256f));
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 parentOffset = gameObject.transform.position;
            Vector3 tileSizeOffset = new Vector3(tileSize/2, tileSize/2);
            Vector3 offset = -parentOffset + tileSizeOffset;
            Vector3 clickCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            (int, int) clickedTile = tileMap.GetGridCoords(clickCoords + offset);
            tileMap.SetTile(clickedTile.Item1, clickedTile.Item2, 1);
            tiles[clickedTile.Item1, clickedTile.Item2].SetText("1");
            tiles[clickedTile.Item1, clickedTile.Item2].SetColor(Color.green);
        }
    }

    private Tile CreateTile(string name, Vector3 position, Vector3 scale) {
        GameObject tileGameObject = Instantiate(tilePrefab);
        tileGameObject.name = name;
        tileGameObject.transform.parent = gameObject.transform;
        tileGameObject.transform.localPosition = position;
        tileGameObject.transform.localScale = scale;
        Tile tileController = tileGameObject.AddComponent<Tile>();
        return tileController;
        
    }
}

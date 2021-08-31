using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private TileMap<int> tileMap;
    private Tile[,] tiles;
    private Coords<int> selectedTile;
    private bool isTileSelected;

    private GameObject[] characters;

    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1;
    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private GameObject player;
    [SerializeField] private int startingX;
    [SerializeField] private int startingY;

    // Start is called before the first frame update
    void Start() {
        tileMap = new TileMap<int>(width, height, tileSize, -1);
        tiles = new Tile[width, height];

        selectedTile = new Coords<int>();
        isTileSelected = false;

        // Render board
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Tile tile = CreateTile(
                    string.Format("Tile {0}, {1}", i, j),
                    tileMap.GetWorldCoords(i, j),
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

        characters = new GameObject[10];

        // Setup player
        int playerIndex = 0;
        Coords<int> startingCoords = new Coords<int>(startingX, startingY);
        characters[playerIndex] = player;
        tileMap.SetTile(startingCoords, playerIndex);
        MoveCharacter(playerIndex, startingCoords);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 parentOffset = gameObject.transform.position;
            Vector3 tileSizeOffset = new Vector3(tileSize/2, tileSize/2);
            Vector3 offset = -parentOffset + tileSizeOffset;
            Vector3 clickCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Coords<int> clickedTile = tileMap.GetGridCoords(clickCoords + offset);
            OnClick(clickedTile);
        }
    }

    private void MoveCharacter(int characterIndex, Coords<int> newPosition) {
        GameObject character = characters[characterIndex];
        character.transform.localPosition = tileMap.GetWorldCoords(newPosition);
    }

    private void OnClick(Coords<int> clickedTile) {
        if (!tileMap.InBounds(clickedTile)) {
            return;
        }

        int characterIndex = tileMap.GetTile(clickedTile);
        if (characterIndex >= 0) {
            GameObject selectedCharacter = characters[characterIndex];
            SpriteRenderer spriteRenderer = selectedCharacter.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
        }

        // if (!isTileSelected) {
        //     tiles[clickedTile.X, clickedTile.Y].SelectTile();
        //     isTileSelected = true;
        //     selectedTile = clickedTile;
        // } else if (selectedTile.Equals(clickedTile)) {
        //     tiles[selectedTile.X, selectedTile.Y].UnselectTile();
        //     isTileSelected = false;
        // } else {
        //     tiles[selectedTile.X, selectedTile.Y].UnselectTile();

        //     tiles[clickedTile.X, clickedTile.Y].SelectTile();
        //     selectedTile = clickedTile;
        // }
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

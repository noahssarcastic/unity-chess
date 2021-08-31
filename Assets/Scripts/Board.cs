using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private TileMap<int> characterMap;
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

    [SerializeField] private GameObject player2;
    [SerializeField] private int startingX2;
    [SerializeField] private int startingY2;

    // Start is called before the first frame update
    void Start() {
        characters = new GameObject[10];
        characterMap = new TileMap<int>(width, height, tileSize, -1);
        tiles = new Tile[width, height];

        selectedTile = new Coords<int>();
        isTileSelected = false;

        // Render board
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Tile tile = CreateTile(
                    string.Format("Tile {0}, {1}", i, j),
                    characterMap.GetWorldCoords(i, j),
                    new Vector3(tileSize, tileSize)
                );
                tiles[i, j] = tile;

                // Create checkerboard pattern
                if ((i + j) % 2 == 0) {
                    tile.SetColor(new Color(87/256f, 58/256f, 46/256f));
                } else {
                    tile.SetColor(new Color(252/256f, 204/256f, 116/256f));
                }
            }
        }

        // Setup player
        int playerIndex = 0;
        Coords<int> startingCoords = new Coords<int>(startingX, startingY);
        characters[playerIndex] = player;
        characterMap.SetTile(startingCoords, playerIndex);
        MoveCharacter(playerIndex, startingCoords);

        // Setup player 2
        int playerIndex2 = 1;
        Coords<int> startingCoords2 = new Coords<int>(startingX2, startingY2);
        characters[playerIndex2] = player2;
        characterMap.SetTile(startingCoords2, playerIndex2);
        MoveCharacter(playerIndex2, startingCoords2);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 parentOffset = gameObject.transform.position;
            Vector3 tileSizeOffset = new Vector3(tileSize/2, tileSize/2);
            Vector3 offset = -parentOffset + tileSizeOffset;
            Vector3 clickCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Coords<int> clickedTile = characterMap.GetGridCoords(clickCoords + offset);
            OnClick(clickedTile);
        }
    }

    private void MoveCharacter(int characterIndex, Coords<int> newPosition) {
        GameObject character = characters[characterIndex];
        character.transform.localPosition = characterMap.GetWorldCoords(newPosition);
    }

    private void OnClick(Coords<int> clickedTile) {
        if (!characterMap.InBounds(clickedTile)) {
            return;
        }

        int clickedTileValue = characterMap.GetTile(clickedTile);
        bool isCharacter = clickedTileValue >= 0;
        bool isSameTile = selectedTile.Equals(clickedTile);

        if (isTileSelected) {
            if (isSameTile) {
                // Unselect tile
                Unselect();
                return;
            }

            if (!isSameTile && isCharacter) {
                // Switch selection
                Unselect();
                SelectTile(clickedTile);
                return;
            }

            if (!isSameTile && !isCharacter) {
                // Move character
                int selectedTileValue = characterMap.GetTile(selectedTile);
                characterMap.SetTile(selectedTile, -1);
                characterMap.SetTile(clickedTile, selectedTileValue);
                Unselect();
                MoveCharacter(selectedTileValue, clickedTile);
                return;
            }
        } else {
            if (isCharacter) {
                // Select tile
                SelectTile(clickedTile);
                return;
            }
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

    private Tile GetTile(int x, int y) {
        return tiles[x, y];
    }

    private Tile GetTile(Coords<int> coords) {
        return tiles[coords.X, coords.Y];
    }

    private void SelectTile(Coords<int> coords) {
        GetTile(coords).SelectTile();
        selectedTile = coords;
        isTileSelected = true;
    }

    private void Unselect() {
        tiles[selectedTile.X, selectedTile.Y].UnselectTile();
        isTileSelected = false;
    }
}

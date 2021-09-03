using UnityEngine;

public class Board : MonoBehaviour, ISaveable {
    private TileMap<int> characterMap;
    private Tile[,] tiles;
    private Coords<int> selectedTile;
    private bool isTileSelected;

    private Character[] characters;
    private int numCharacters;

    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1;
    [SerializeField] private GameObject tilePrefab;

    // Start is called before the first frame update
    void Start() {
        characters = new Character[10];
        numCharacters = 0;
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


        SpawnCharacter(new Coords<int>(0, 0), ClassName.King);
        SpawnCharacter(new Coords<int>(0, 1), ClassName.Queen);
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
        Character character = characters[characterIndex];
        character.Move(characterMap.GetWorldCoords(newPosition));
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

    public void SpawnCharacter(Coords<int> startingPosition, ClassName className) {
        if (numCharacters > characters.Length) {
            Debug.LogError("Too many characters, cannot spawn more.");
            return;
        }

        if (characterMap.GetTile(startingPosition) >= 0) {
            Debug.LogError("Cannot spawn character on top of another.");
            return;
        }

        // Create character object
        GameObject characterPrefab = CharacterClass.GetClassPrefab(className);
        GameObject characterGameObject = Instantiate(characterPrefab);
        Character character = characterGameObject.AddComponent<Character>();
        character.Move(characterMap.GetWorldCoords(startingPosition));

        // Add character to board and characterMap
        characterMap.SetTile(startingPosition, numCharacters);
        characters[numCharacters] = character;
        numCharacters++;
    }

    public void SpawnCharacter(CharacterData characterData) {
        Coords<int> startingPosition = characterData.position;
        ClassName className = characterData.className;
        SpawnCharacter(startingPosition, className);
    }

    public void PopulateSaveData(SaveData saveData) {
        // foreach (var character in characters) {
        //     if (character is null) continue;
        //     Coords<int> characterPosition = characterMap.GetGridCoords(character.GetPosition());
        //     saveData.characters.Add(new CharacterData() {position=characterPosition});
        // }
    }

    public void LoadFromSaveData(SaveData a_SaveData) {

    }
}

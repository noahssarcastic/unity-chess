using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private TileMap tileMap;
    private GameObject[,] tiles;

    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;

    [SerializeField] private Sprite tileSprite;

    // Start is called before the first frame update
    void Start() {
        tileMap = new TileMap(width, height, 1);
        tiles = new GameObject[width, height];

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // Create tile
                GameObject tile = new GameObject(string.Format("Tile {0}, {1}", i, j), typeof(SpriteRenderer));
                tile.transform.parent = gameObject.transform;
                tile.transform.localPosition = new Vector3(i, j);

                // Render tile
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                sr.sprite = tileSprite;
                if ((i+j) % 2 == 0) {
                    sr.color = new Color(87/256f, 58/256f, 46/256f);
                } else {
                    sr.color = new Color(252/256f, 204/256f, 116/256f);
                }

                // Create debug textmesh
                TextMesh tm = WorldText.CreateWorldText(
                    tileMap.GetTile(i, j).ToString(), 
                    tileMap.GetWorldCoords(i, j) + gameObject.transform.position, 
                    Color.white);
                tm.transform.parent = tile.transform;

                tiles[i, j] = tile;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 offset = gameObject.transform.position;
            Vector3 clickCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            (int, int) clickedTile = tileMap.GetGridCoords(clickCoords);
            tileMap.SetTile(clickedTile.Item1, clickedTile.Item2, 1);
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                GameObject tile = tiles[i, j];
                tile.GetComponentInChildren<TextMesh>().text = tileMap.GetTile(i, j).ToString();
            }
        }
    }

    private void CreateTile() {

    }
}

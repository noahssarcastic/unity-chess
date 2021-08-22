using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private TileMap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = gameObject.transform.transform.position;
        tileMap = new TileMap(8, 8, 1);
        for (int i = 0; i < tileMap.Width; i++)
        {
            for (int j = 0; j < tileMap.Height; j++)
            {
                WorldText.CreateWorldText(
                    tileMap.GetTile(i, j).ToString(), 
                    tileMap.GetWorldCoords(i, j) + offset, 
                    Color.white);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

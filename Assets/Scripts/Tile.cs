using UnityEngine;

public class Tile : MonoBehaviour
{
    private TextMesh textMesh;
    private SpriteRenderer sprite;

    void Awake() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        // Create debug textmesh
        textMesh = WorldText.CreateWorldText(
            "404", 
            gameObject.transform.position, 
            Color.white);
        textMesh.transform.parent = gameObject.transform;
    }

    public void SetColor(Color newColor) {
        sprite.color = newColor;
    }

    public void SetText(string text) {
        textMesh.text = text;
    }
    
}

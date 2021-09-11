using UnityEngine;

public class Tile : MonoBehaviour, IHasDetails
{
    private TextMesh textMesh;
    private SpriteRenderer sprite;
    private bool isSelected = false;
    private Color unselectedColor = Color.white;
    private Color selectedColor = Color.green;

    void Awake() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        // Create debug textmesh
        // textMesh = WorldText.CreateWorldText(
        //     "404", 
        //     gameObject.transform.position, 
        //     Color.white);
        // textMesh.transform.parent = gameObject.transform;
    }

    public void SetColor(Color newColor) {
        unselectedColor = newColor;
        if (!isSelected) 
            sprite.color = newColor;
    }

    public void SetText(string text) {
        // textMesh.text = text;
    }

    public void SelectTile() {
        isSelected = true;
        sprite.color = selectedColor;
    }

    public void UnselectTile() {
        isSelected = false;
        sprite.color = unselectedColor;
    }

    public string GetDetails() {
        return string.Format("A {0} tile. Not much else here.", unselectedColor);
    }
    
}

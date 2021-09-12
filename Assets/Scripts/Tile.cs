using UnityEngine;
using UnityEditor;


public class Tile: MonoBehaviour, IHasDetails {

    private SpriteRenderer spriteRenderer;
    private bool isSelected = false;
    private Color unselectedColor = Color.white;
    private Color selectedColor = Color.green;
    private string _debugText = "";

    void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    void OnDrawGizmos() {
        Handles.Label(transform.position, _debugText);
    }

    public void SetColor(Color newColor) {
        unselectedColor = newColor;
        if (!isSelected) 
            spriteRenderer.color = newColor;
    }

    public void SetText(string text) {
        _debugText = text;
    }

    public void SelectTile() {
        isSelected = true;
        spriteRenderer.color = selectedColor;
    }

    public void UnselectTile() {
        isSelected = false;
        spriteRenderer.color = unselectedColor;
    }

    public string GetDetails() {
        return string.Format("A tile at {0}. Not much else here.", transform.position);
    }
    
}

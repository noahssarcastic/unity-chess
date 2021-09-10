using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PromptChoice : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler {

    private TextMeshProUGUI textMesh;
    private FontStyles defaultFontStyle;

    public void Awake() {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        defaultFontStyle = textMesh.fontStyle;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.fontStyle = defaultFontStyle;
    }

}

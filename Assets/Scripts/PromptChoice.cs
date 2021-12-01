using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;


public class PromptChoice : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler {

    private TextMeshProUGUI textMesh;
    private FontStyles defaultFontStyle;
    private UnityAction OnClickHandler;

    public void Awake() {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        defaultFontStyle = textMesh.fontStyle;
        OnClickHandler = () => {};
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClickHandler();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        textMesh.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData) {
        textMesh.fontStyle = defaultFontStyle;
    }

    public void SetChoice(string text="", UnityAction handler=null) {
        if (handler == null) OnClickHandler = () => {};
        else OnClickHandler = handler;

        textMesh.text = text;
    }
}

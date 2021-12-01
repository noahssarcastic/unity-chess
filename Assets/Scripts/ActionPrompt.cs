using UnityEngine;
using TMPro;


public interface IHasDetails {
    public string GetDetails();
}


public class ActionPrompt: MonoBehaviour {

    [SerializeField] TextMeshProUGUI prompt;
    [SerializeField] PromptChoice choiceOne;
    [SerializeField] PromptChoice choiceTwo;
    [SerializeField] PromptChoice choiceThree;
    [SerializeField] PromptChoice choiceFour;

    public void Start() {
        Initialize();
        EventManager.AddListener(EventName.SelectTile, HandleSelectTile);
        EventManager.AddListener(EventName.UnselectTile, HandleUnselectTile);

    }

    private void Initialize() {
        prompt.text = "Select a tile...";
        choiceOne.SetChoice("");
        choiceTwo.SetChoice("");
        choiceThree.SetChoice("");
        choiceFour.SetChoice("");
    }

    public void HandleSelectTile() {
        IHasDetails selectedObject = GameManager.Instance.Board.GetSelectedObject();
        prompt.text = selectedObject.GetDetails();
        if (selectedObject is Character) {
            choiceOne.SetChoice("Move", HandleChooseMove);
            choiceTwo.SetChoice("Attack");
            choiceThree.SetChoice("Special");
            choiceFour.SetChoice("More...");
        } else {
            choiceOne.SetChoice("");
            choiceTwo.SetChoice("");
            choiceThree.SetChoice("");
            choiceFour.SetChoice("");
        }
    }

    public void HandleUnselectTile() {
        IHasDetails selectedObject = GameManager.Instance.Board.GetSelectedObject();
        prompt.text = "Select a tile...";
        choiceOne.SetChoice("");
        choiceTwo.SetChoice("");
        choiceThree.SetChoice("");
        choiceFour.SetChoice("");
    }

    public void HandleChooseMove() {
        EventManager.Invoke(EventName.MoveSelectedCharacter);
    }
}

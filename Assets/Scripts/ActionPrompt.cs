using UnityEngine;
using TMPro;

public class ActionPrompt: MonoBehaviour {
    [SerializeField] TextMeshProUGUI prompt;
    [SerializeField] TextMeshProUGUI choiceOne;
    [SerializeField] TextMeshProUGUI choiceTwo;
    [SerializeField] TextMeshProUGUI choiceThree;
    [SerializeField] TextMeshProUGUI choiceFour;

    public void Awake() {

    }

    public void Start() {
        prompt.text = "Prompting...";
        choiceOne.text = "Choice 1";
        choiceTwo.text = "Choice 2";
        choiceThree.text = "Choice 3";
        choiceFour.text = "Choice 4";
    }
}
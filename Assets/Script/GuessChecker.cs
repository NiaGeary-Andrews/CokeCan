using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuessChecker : MonoBehaviour
{
    public TMP_InputField guess;
    private string answer = "dishwasher";
    public Button submitButton;
    public Button playSoundButton;
    public TextMeshProUGUI outputText;

    public void OnSubmitted()
    {
        string inputText = guess.text;
        inputText =  inputText.Replace(" ", "");

        if (inputText.ToLower() == answer)
        {
            Debug.Log("Correct!");
            outputText.text = "Correct! The number you need is 8!";
            guess.interactable = false;
            submitButton.interactable = false;
        }
        else
        {
            Debug.Log("Incorrect");
            outputText.text = "Incorrect! Try again!";
        }
    }
}

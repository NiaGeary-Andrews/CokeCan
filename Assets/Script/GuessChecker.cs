using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuessChecker : MonoBehaviour
{
    public TMP_InputField guess;
    public string answer = "hehe";
    public Button submitButton;

    public void OnSubmitted()
    {
        string inputText = guess.text;
        inputText =  inputText.Replace(" ", "");

        if (inputText.ToLower() == answer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}

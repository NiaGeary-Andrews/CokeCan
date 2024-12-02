using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MultiAnswerManager : MonoBehaviour
{
    public TMP_InputField guess1;
    public TMP_InputField guess2;
    public TMP_InputField guess3;
    public TMP_InputField guess4;
    public TMP_InputField guess5;

    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public string answer5;

    public Button submitButton;
    public TextMeshProUGUI outputText;

    public void OnSubmitted()
    {
        // Clean and retrieve user input for all fields
        string inputText1 = guess1.text.Replace(" ", "").ToLower();
        string inputText2 = guess2.text.Replace(" ", "").ToLower();
        string inputText3 = guess3.text.Replace(" ", "").ToLower();
        string inputText4 = guess4.text.Replace(" ", "").ToLower();
        string inputText5 = guess5.text.Replace(" ", "").ToLower();

        // Check if all inputs are correct
        if (inputText1 == answer1.ToLower() &&
            inputText2 == answer2.ToLower() &&
            inputText3 == answer3.ToLower() &&
            inputText4 == answer4.ToLower() &&
            inputText5 == answer5.ToLower())
        {
            Debug.Log("Correct!");
            outputText.text = "Correct! The number you need is 8!";

            // Disable interaction with inputs and the submit button
            guess1.interactable = false;
            guess2.interactable = false;
            guess3.interactable = false;
            guess4.interactable = false;
            guess5.interactable = false;
            submitButton.interactable = false;

            // Unlock levels and save progress
            if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            Debug.Log("Incorrect");
            outputText.text = "Incorrect! Try again!";
        }
    }
}

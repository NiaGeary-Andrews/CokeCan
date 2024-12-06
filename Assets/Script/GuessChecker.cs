using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuessChecker : MonoBehaviour
{
    public TMP_InputField guess;
    public string answer;
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
            outputText.text = "Correct!";
            guess.interactable = false;
            submitButton.interactable = false;
            AudioManager.PlaySound(SoundType.UNLOCK);
            LevelStateManager.LockLevel(SceneManager.GetActiveScene().buildIndex -1);
        }
        else
        {
            Debug.Log("Incorrect");
            outputText.text = "Incorrect! Try again!";
        }
    }
}

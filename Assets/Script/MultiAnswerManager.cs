using TMPro;
using TMPro.EditorUtilities;
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
    public TMP_InputField guess6;

    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public string answer5;
    public string answer6;

    public RectTransform panel1;
    public RectTransform panel2;

    public Button submitButton;
    public TextMeshProUGUI outputText;
    public bool firstStage = true;

    public void Start()
    {
        panel1.gameObject.SetActive(true);
        panel2.gameObject.SetActive(false);

    }

    public void OnSubmitted()
    {
        if (firstStage) {
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
                outputText.text = "Correct! ID the snack in the microscope and enter in the snack number";
                AudioManager.PlaySound(SoundType.UNLOCK);

                // Disable interaction with inputs and the submit button
                guess1.interactable = false;
                guess2.interactable = false;
                guess3.interactable = false;
                guess4.interactable = false;
                guess5.interactable = false;
                //LevelStateManager.LockLevel(SceneManager.GetActiveScene().buildIndex - 1);

                panel1.gameObject.SetActive(false);
                panel2.gameObject.SetActive(true);

            }
            else
            {
                Debug.Log("Incorrect");
                outputText.text = "Incorrect! Try again!";
                guess1.text = "";
                guess2.text = "";
                guess3.text = "";
                guess4.text = "";
                guess5.text = "";
                AudioManager.PlaySound(SoundType.INCORRECT);
            }

            firstStage = false;
        }

        else
        {
            // Clean and retrieve user input for all fields
            string inputText6 = guess6.text.Replace(" ", "").ToLower();

            if (inputText6 == answer6.ToLower())
            {
                outputText.text = "Correct!";
                LevelStateManager.LockLevel(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

    }
}

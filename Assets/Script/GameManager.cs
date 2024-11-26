using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI outputText;   // Text field to show the status
    public Button[] wordButtons;       // Buttons for words
    public Button resetButton;           // Button to reset the code
    public string[] correctGroups;        // Correct sequence of button presses
    private string guessInput = "";       // Code input by the player
    public int numButtonPressed = 0;     // Counter for button pressed

    void Start()
    {
        foreach (Button btn in wordButtons)
        {
            btn.onClick.AddListener(() => OnNumberButtonPressed(btn));
        }
        resetButton.onClick.AddListener(ResetCode);
    }
    void OnNumberButtonPressed(Button clickedButton)
    {
        if (numButtonPressed >= 4)
        {
            return; // Prevent further input if 4 buttons have already been pressed
        }

        numButtonPressed++;

        // Append the number based on button name (e.g., "Button1" -> "1")
        string buttonPressed = clickedButton.name.Replace("Button", "");
        guessInput += buttonPressed;

        Color buttonColor = clickedButton.GetComponent<Image>().color;

        AudioManager.PlaySound(SoundType.BUTTONCLICK);

        //if (numButtonPressed == correctCode.Length)
        //{
        //    // Here is where the submit button becomes active and you can submit your guess



        //    //if (inputCode == correctCode)
        //    //{
        //    //    Unlock();
        //    //}
        //    //else
        //    //{
        //    //    Invoke("ResetCode", 1f);
        //    //    outputText.text = "INCORRECT, Try again";
        //    //    AudioManager.PlaySound(SoundType.INCORRECT);
        //    //}
        //}
    }

    void Unlock()
    {
        Debug.Log("Code Correct! You unlocked it!");
        outputText.text = "CORRECT, The number you need is 8";
        AudioManager.PlaySound(SoundType.UNLOCK);
    }

    void ResetCode()
    {
        //inputCode = "";
        //numButtonPressed = 0;
        //outputText.text = "Enter in the four colour code";

        //// Reset all images in the strip to white
        //foreach (Image img in images)
        //{
        //    img.color = Color.white;
        //}

        //Debug.Log("Code reset");
        //AudioManager.PlaySound(SoundType.RESTART);
    }

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    public TextMeshProUGUI outputText;   // Text field to show the status
    public Button[] numberButtons;       // Buttons for colors
    public Button resetButton;           // Button to reset the code
    public string correctCode = "1234";  // Correct sequence of button presses
    private string inputCode = "";       // Code input by the player
    public int numButtonPressed = 0;     // Counter for button presses
    public Image[] images;               // Strip of images to show pressed colors

    void Start()
    {
        foreach (Button btn in numberButtons)
        {
            btn.onClick.AddListener(() => OnNumberButtonPressed(btn));
        }
        resetButton.onClick.AddListener(ResetCode);
    }
    void OnNumberButtonPressed(Button clickedButton)
    {
        if (numButtonPressed >= images.Length)
        {
            return; // Prevent further input if the strip is already full
        }

        numButtonPressed++;

        // Append the number based on button name (e.g., "Button1" -> "1")
        string buttonPressed = clickedButton.name.Replace("Button", "");
        inputCode += buttonPressed;

        Color buttonColor = clickedButton.GetComponent<Image>().color;

        // Update the corresponding image in the strip
        images[numButtonPressed - 1].color = buttonColor;

        AudioManager.PlaySound(SoundType.BUTTONCLICK);

        if (numButtonPressed == correctCode.Length)
        {
            if (inputCode == correctCode)
            {
                Unlock();
            }
            else
            {
                Invoke("ResetCode", 1f);
                outputText.text = "INCORRECT, Try again";
                AudioManager.PlaySound(SoundType.INCORRECT);
            }
        }
    }

    void Unlock()
    {
        Debug.Log("Code Correct! You unlocked it!");
        outputText.text = "CORRECT, The number word you need is ******";
        AudioManager.PlaySound(SoundType.UNLOCK);
        // Example: Lock this level when complete
        LevelStateManager.LockLevel(0);
    }

    void ResetCode()
    {
        inputCode = "";
        numButtonPressed = 0;
        outputText.text = "Enter in the four colour code"; 

        // Reset all images in the strip to white
        foreach (Image img in images)
        {
            img.color = Color.white;
        }

        Debug.Log("Code reset");
        AudioManager.PlaySound(SoundType.RESTART);
    }
}

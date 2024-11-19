using UnityEngine;
using UnityEngine.UI; // For UI elements like Buttons, Text
using TMPro; // For TextMeshPro

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
        // Add listeners to each button to call the appropriate function when clicked
        foreach (Button btn in numberButtons)
        {
            btn.onClick.AddListener(() => OnNumberButtonPressed(btn));
        }

        // Add listener to the reset button
        resetButton.onClick.AddListener(ResetCode);
    }

    // Called when a number button is pressed
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

        // Get the button's color
        Color buttonColor = clickedButton.GetComponent<Image>().color;

        // Update the corresponding image in the strip
        images[numButtonPressed - 1].color = buttonColor;

        AudioManager.PlaySound(SoundType.BUTTONCLICK);

        // Check if the input matches the correct code
        if (numButtonPressed == correctCode.Length)
        {
            if (inputCode == correctCode)
            {
                Unlock(); // Unlock if the code is correct
            }
            else
            {
                // If incorrect, reset after a delay
                Invoke("ResetCode", 1f);
                outputText.text = "INCORRECT, Try again";
                AudioManager.PlaySound(SoundType.INCORRECT);
            }
        }
    }

    // Function to handle unlocking logic
    void Unlock()
    {
        Debug.Log("Code Correct! You unlocked it!");
        outputText.text = "CORRECT, The number you need is 8";
        AudioManager.PlaySound(SoundType.UNLOCK);
    }

    // Function to reset the input code and images
    void ResetCode()
    {
        inputCode = "";
        numButtonPressed = 0;
        outputText.text = "Enter in the four colour code"; // Clear the displayed status

        // Reset all images in the strip to white
        foreach (Image img in images)
        {
            img.color = Color.white;
        }

        Debug.Log("Code reset");
        AudioManager.PlaySound(SoundType.RESTART);
    }
}

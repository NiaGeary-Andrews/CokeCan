using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI outputText;   // Text field to show the status
    public TextMeshProUGUI titleText;   // Text field to show the completion
    public Button[] wordButtons;       // Buttons for words
    public Button deselectAllButton;           // Button to reset the code
    public Button submitButton;
    private List<string> selectedButtons = new List<string>();
    public int numButtonPressed = 0;     // Counter for button pressed
    private Dictionary<string, Button> buttonLookup = new Dictionary<string, Button>();
    private Color defaultColor = new Color32 (162, 155, 254, 255); // Default button color
    private Color selectedColor = Color.gray; // Highlight color for selected buttons
    private bool isCorrectGroup;
    private int numCorrectGroups;


    // Define the groups
    private Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>()
    {
        { "Group1", new List<string> { "A1", "B4", "D1", "D4" } }, //Characters from christmas movies - YELLOW
        { "Group2", new List<string> { "A3", "B2", "C4", "D3" } }, //Mr ___ -- GREEN
        { "Group3", new List<string> { "C2", "C3", "D2", "A4" } }, //Feel around for - BLUE
        { "Group4", new List<string> { "C1", "A2", "B1", "B3" } } //Words associated with snow or winter - PURPLE
    };

    private Dictionary<string, Color> groupColors = new Dictionary<string, Color>()
{
    { "Group1", Color.yellow }, // Characters from christmas movies
    { "Group2", Color.green },  // Mr ___
    { "Group3", Color.blue },   // Feel around for
    { "Group4", new Color(0.5f, 0f, 0.5f) } // Words associated with snow or winter
};


    private Dictionary<string, string> groupNames = new Dictionary<string, string>()
{
    { "Group1", "Characters from christmas movies" }, // Characters from christmas movies
    { "Group2", "Mr _____" },  // Mr ___
    { "Group3", "Feel around for" },   // Feel around for
    { "Group4", "Words associated with snow or winter" } //Words associated with snow or winter
};

    void Start()
    {
        foreach (Button btn in wordButtons)
        {
            string buttonId = btn.name; // Button name corresponds to its ID
            buttonLookup[buttonId] = btn; // Add to lookup for easy access
            btn.onClick.AddListener(() => OnNumberButtonPressed(btn));
        }
        deselectAllButton.onClick.AddListener(ResetSelection);
        submitButton.onClick.AddListener(Submit);

    }
    void OnNumberButtonPressed(Button clickedButton)
    {
        string buttonId = clickedButton.name; // Ensure each button's name matches its group ID

        if (selectedButtons.Contains(buttonId))
        {
            // If already selected, deselect it
            selectedButtons.Remove(buttonId);
            clickedButton.image.color = defaultColor; // Reset to default color
            Debug.Log($"Deselected {buttonId}");
        }
        else
        {
            if (selectedButtons.Count <= 3)
            {
                // Add to selection if not already selected
                selectedButtons.Add(buttonId);
                clickedButton.image.color = selectedColor; // Highlight as selected
                Debug.Log($"Selected {buttonId}");
            }
        }

    }

    void Submit()
    {
        // Check if 4 buttons are selected
        if (selectedButtons.Count == 4)
        {
            CheckGroup();
        }
    }

    void CheckGroup()
    {
        foreach (var group in groups)
        {
            if (IsMatchingGroup(group.Value))
            {
                Debug.Log($"Correct! These buttons form {group.Key}");
                HighlightSolvedGroup(group.Key);
                return;
            }
        }

        isCorrectGroup = false;

        // If no complete group is found, check for partial matches (3 in a group)
        if (CheckPartialGroup())
        {
            Debug.Log("You have 3 correct buttons from a group!");
            titleText.text = "You have 3 correct buttons from a group! Keep going!";
            //AudioManager.PlaySound(SoundType.PARTIAL); // Optional sound for partial success
            AudioManager.PlaySound(SoundType.INCORRECT);
        }
        else
        {
            Debug.Log("Incorrect group! Try again.");
            titleText.text = "Incorrect group! Try again.";
            AudioManager.PlaySound(SoundType.INCORRECT);
        }

        // Reset selection since no complete match was found
        ResetSelection();
    }

    bool IsMatchingGroup(List<string> group)
    {
        foreach (string buttonId in selectedButtons)
        {
            if (!group.Contains(buttonId))
                return false;
        }
        return true;
    }

    void HighlightSolvedGroup(string groupName)
    {
        // Get the color for the group
        Color solvedGroupColor = groupColors[groupName];
        string solvedGroupName = groupNames[groupName];
        numCorrectGroups += 1;

        foreach (string buttonId in selectedButtons)
        {
            if (buttonLookup.TryGetValue(buttonId, out Button button))
            {
                // Highlight as solved with the specific group color
                button.image.color = solvedGroupColor;

                // Disable interaction
                button.interactable = false;
            }
        }

        isCorrectGroup = true;

        // Convert color to a hex string for Rich Text
        string hexColor = ColorUtility.ToHtmlStringRGB(solvedGroupColor);

        // Display the solved group name in the output text with Rich Text coloring
        outputText.text += $"<b><color=#{hexColor}>• {solvedGroupName}</color></b>\n";
        titleText.text = "Correct!";

        if (numCorrectGroups == 4) {
            titleText.text = "Well done you have unlocked room 6!";
            submitButton.interactable = false;
            deselectAllButton.interactable = false;
            LevelStateManager.LockLevel(SceneManager.GetActiveScene().buildIndex - 1);
            LevelStateManager.UnlockLevel(5);
        }

        AudioManager.PlaySound(SoundType.UNLOCK);
        ResetSelection(); // Clear the selection list for the next group
    }

    void ResetSelection()
    {
        if (!isCorrectGroup)
        {
            foreach (string buttonId in selectedButtons)
            {
                if (buttonLookup.TryGetValue(buttonId, out Button button))
                {
                    // Highlight as solved
                    button.image.color = defaultColor;
                }
            }
        }
        selectedButtons.Clear();
        isCorrectGroup= false;
    }

    bool CheckPartialGroup()
    {
        foreach (var group in groups)
        {
            int matchingCount = 0;

            foreach (string buttonId in selectedButtons)
            {
                if (group.Value.Contains(buttonId))
                {
                    matchingCount++;
                }
            }

            if (matchingCount == 3)
            {
                // Found 3 matching buttons in the same group
                return true;
            }
        }

        return false;
    }

}

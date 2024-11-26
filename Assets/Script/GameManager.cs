using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI outputText;   // Text field to show the status
    public Button[] wordButtons;       // Buttons for words
    public Button deselectAllButton;           // Button to reset the code
    private List<string> selectedButtons = new List<string>();
    public int numButtonPressed = 0;     // Counter for button pressed


    // Define the groups
    private Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>()
    {
        { "Group1", new List<string> { "A1", "B4", "D1", "D4" } }, //Elements in periodic table
        { "Group2", new List<string> { "A3", "B2", "C4", "D3" } }, //Metabolism concepts
        { "Group3", new List<string> { "C2", "C3", "D2", "A4" } }, //Dynamic Scientific process
        { "Group4", new List<string> { "C1", "A2", "B1", "B3" } } //Biochemical Terms
    };

    void Start()
    {
        foreach (Button btn in wordButtons)
        {
            btn.onClick.AddListener(() => OnNumberButtonPressed(btn));
        }
        deselectAllButton.onClick.AddListener(ResetSelection);
    }
    void OnNumberButtonPressed(Button clickedButton)
    {
        string buttonId = clickedButton.name; // Ensure each button's name matches its group ID

        if (selectedButtons.Contains(buttonId))
        {
            // If already selected, deselect it
            selectedButtons.Remove(buttonId);
            Debug.Log($"Deselected {buttonId}");
        }
        else
        {
            // Add to selection if not already selected
            selectedButtons.Add(buttonId);
            Debug.Log($"Selected {buttonId}");
        }

        // Check if 4 buttons are selected
        if (selectedButtons.Count == 4)
        {
            CheckGroup();
        }
    }

    void Submit()
    {
       
    }

    void CheckGroup()
    {
        // Check if the selected buttons form a valid group
        foreach (var group in groups)
        {
            if (IsMatchingGroup(group.Value))
            {
                Debug.Log($"Correct! These buttons form {group.Key}");
                ResetSelection();
                return;
            }
        }

        Debug.Log("Incorrect group! Try again.");
        ResetSelection();
    }

    bool IsMatchingGroup(List<string> group)
    {
        // Check if all selected buttons are in the same group
        foreach (string buttonId in selectedButtons)
        {
            if (!group.Contains(buttonId))
                return false;
        }
        return true;
    }

    void ResetSelection()
    {
        selectedButtons.Clear();
    }

}

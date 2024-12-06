using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    public RectTransform panel1;

    // Texts for unlocked and locked states
    public string[] unlockedTexts = { "Challenge 1", "Challenge 2", "Challenge 3", "Challenge 4", "Challenge 5", "Challenge 6" };
    public string[] lockedTexts = { "Word", "0", "8", "-", "3", "1" };

    private void Awake()
    {
        //DELETE ALL PLAYER PREFS
        //PlayerPrefs.DeleteAll();
        panel1.gameObject.SetActive(false);
        ButtonsToArray();
        ApplyStatesAndTextsToButtons();

        if (isCompleted())
        {
            panel1.gameObject.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }

    private void ApplyStatesAndTextsToButtons()
    {
        // Fetch states from LevelStateManager
        bool[] levelStates = LevelStateManager.GetLevelStates();

        for (int i = 0; i < buttons.Length; i++)
        {
            // Set button interactable based on state
            bool isUnlocked = levelStates[i];
            buttons[i].interactable = isUnlocked;

            // Update the button's text with the appropriate label
            UpdateButtonText(i, isUnlocked ? unlockedTexts[i] : lockedTexts[i]);
        }
    }

    public void UpdateButtonText(int buttonIndex, string newText)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length) return;

        // Get the TextMeshProUGUI component and update its text
        TextMeshProUGUI buttonText = buttons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = newText;
        }
        else
        {
            Debug.LogError($"TextMeshProUGUI not found for button {buttons[buttonIndex].name}");
        }
    }

    public void LockLevel(int levelIndex)
    {
        // Lock the specified level and update its text
        LevelStateManager.LockLevel(levelIndex);
        UpdateButtonText(levelIndex, lockedTexts[levelIndex]);
        buttons[levelIndex].interactable = false;
    }

    public void UnlockLevel(int levelIndex)
    {
        // Unlock the specified level and update its text
        LevelStateManager.UnlockLevel(levelIndex);
        UpdateButtonText(levelIndex, unlockedTexts[levelIndex]);
        buttons[levelIndex].interactable = true;
        if (isCompleted())
        {
            panel1.gameObject.SetActive(true);
        }
    }

    bool isCompleted()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // Set button interactable based on state
            if (buttons[i].IsInteractable())
            {
                return false;
            }
        }
        return true;
    }
}



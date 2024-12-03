using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    private const string LevelStateKey = "LevelState";

    public static void UnlockLevel(int levelIndex)
    {
        string savedStates = PlayerPrefs.GetString(LevelStateKey, "111110"); // Default states
        char[] stateArray = savedStates.ToCharArray();

        if (levelIndex >= 0 && levelIndex < stateArray.Length)
        {
            stateArray[levelIndex] = '1'; // Mark level as unlocked
        }

        PlayerPrefs.SetString(LevelStateKey, new string(stateArray));
        PlayerPrefs.Save();
    }

    public static void LockLevel(int levelIndex)
    {
        string savedStates = PlayerPrefs.GetString(LevelStateKey, "111110"); // Default states
        char[] stateArray = savedStates.ToCharArray();

        if (levelIndex >= 0 && levelIndex < stateArray.Length)
        {
            stateArray[levelIndex] = '0'; // Mark level as locked
        }

        PlayerPrefs.SetString(LevelStateKey, new string(stateArray));
        PlayerPrefs.Save();
    }

    public static bool[] GetLevelStates()
    {
        string savedStates = PlayerPrefs.GetString(LevelStateKey, "111110");
        bool[] levelStates = new bool[savedStates.Length];
        for (int i = 0; i < savedStates.Length; i++)
        {
            levelStates[i] = savedStates[i] == '1'; // '1' = unlocked, '0' = locked
        }
        return levelStates;
    }
}


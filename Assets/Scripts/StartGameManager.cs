using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    private void Start()
    {
        int unlockedLevels = PlayerPrefs.GetInt("levelsUnlocked", 1);

        Debug.Log(unlockedLevels);
        if (unlockedLevels > 1)
        {
            SceneManager.LoadScene(unlockedLevels-1);
        }
    }
}

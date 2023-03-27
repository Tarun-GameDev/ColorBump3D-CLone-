using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject deadMenuObj;
    [SerializeField] GameObject startMenuObj;
    [SerializeField] GameObject playingMenuObj;
    [SerializeField] GameObject levelCompletedMenuObj;
    [SerializeField] GameObject ResumeMenuUI;
    [SerializeField] GameObject finalMenuObj;
    [SerializeField] AudioManager audioManager;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if(audioManager == null)
            audioManager = AudioManager.instance;

       
        GameIsPaused = false;
        startMenuObj.SetActive(true);
        playingMenuObj.SetActive(false);
        AudioManager.instance.Play("ThemeSong");
    }

    public void DeadMenu()
    {
        deadMenuObj.SetActive(true);
    }

    public void RestartButton()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.Play("Button");
    }

    public void StartTheGame()
    {
        CameraController.instance.canmove = true;
        Player.instance.startTheGame = true;
        startMenuObj.SetActive(false);
        playingMenuObj.SetActive(true);
    }


    public void Resume()
    {
       
        playingMenuObj.SetActive(true);
        ResumeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioManager.Play("Button");
    }

    public void Pause()
    {
        
        if (Player.instance.playerDead)
            return;
        playingMenuObj.SetActive(false);
        ResumeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioManager.Play("Button");
    }

    public void LevelCompleted()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            //display final ad
            //display final menu
            GoogleAdMobController.instance.ShowInterstitialAd();
            finalMenuObj.SetActive(true);
            PlayerPrefs.SetInt("levelsUnlocked", 10);
        }
        else
        {
            levelCompletedMenuObj.SetActive(true);
            PlayerPrefs.SetInt("levelsUnlocked", SceneManager.GetActiveScene().buildIndex + 2);
            Debug.Log( "fes"+PlayerPrefs.GetInt("levelsUnlocked",1));
        }
    }

    public void NextLevelButton()
    {
        
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioManager.Play("Button");
    }

    public void ReplayButton()
    {
        
        SceneManager.LoadScene(0);
        //reset stats
        audioManager.Play("Button");
    }

    public void ExitButton()
    {
       
        Application.Quit();
        audioManager.Play("Button");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_Ingame_Menu : MonoBehaviour
{
    public Scr_Movement_player MainScript;
    float currentHealth = 100f;
    public AudioSource Music;

    [Header("Canvas Objects")]
    public Slider MasterVolume;
    public float masterVolume;
    public Text MasterVolumeText;
    public Dropdown VideoFullScreen;
    public Dropdown VideoResolution;
    public Slider Fov;
    public Text FovText;
    public Slider MusicVolume;
    float musicVolume;

    //UI from other canvas
    public Text ScoreText;
    public Text HighScoreText;
    public Text FinalScore;

    [Header("Varibles")]
    FullScreenMode settingVid;

    public int score;
    public int highScore;
    public float fov;
    public Camera fpsCam;

    [Header("pannels")]
    public Canvas canvas;
    public GameObject mainPauseMenu;
    public GameObject comfirmQuit;
    public GameObject settingsMenu;
    public GameObject GameOver;

    private bool isShowing = false;

    private void Start()
    {
        //loading save values and if not default values are used instead.
        if (Save_Manager.instance.hasLoaded)
        {
            masterVolume = Save_Manager.instance.activeSave.masterVolumeSave;
            MasterVolume.value = Save_Manager.instance.activeSave.masterVolumeSave;

            highScore = Save_Manager.instance.activeSave.HighScore;

            VideoResolution.value = Save_Manager.instance.activeSave.ScreenResolution;

            VideoFullScreen.value = Save_Manager.instance.activeSave.FullscreenMode;

            Fov.value = Save_Manager.instance.activeSave.FOV;
        }
        else
        {
            Save_Manager.instance.activeSave.masterVolumeSave = 1f;

            Save_Manager.instance.activeSave.FullscreenMode = 4;

            Save_Manager.instance.activeSave.ScreenResolution = 3;

            Save_Manager.instance.activeSave.HighScore = 18002832;

            Save_Manager.instance.activeSave.FOV = 100;
        }

        //sets the menus correctly so there is no bug
        canvas.enabled = false;
        mainPauseMenu.SetActive(false);
        comfirmQuit.SetActive(false);
        settingsMenu.SetActive(false);
        GameOver.SetActive(false);

        //calls the load settins funtion
        LoadSettings();

        //sets highscore.text to highscore and score to 0
        HighScoreText.text = "High Score: " + highScore;
        score = 0;
    }

    private void Update()
    {
        //cursor stuff
        Cursor.visible = isShowing;
        if (isShowing)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (!isShowing)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //pause menu toggle and will disable if the player is dead
        if (Input.GetKeyDown("escape") && currentHealth > 0)
        {
            isShowing = !isShowing;
            canvas.enabled = isShowing;
            mainPauseMenu.SetActive(isShowing);
            comfirmQuit.SetActive(false);
            settingsMenu.SetActive(false);
        }

        //if the menu is showing the game will pause
        if (isShowing == true)
        {
            Time.timeScale = 0;
        }
        else if (isShowing == false)
        {
            Time.timeScale = 1;
        }

        //settings update values
        MasterVolumeText.text = "Master Volume: " + ((int)(masterVolume * 100));
        masterVolume = MasterVolume.value;
        AudioListener.volume = masterVolume;

        FovText.text = "Fov: " + fov;
        fov = Fov.value;
        fpsCam.fieldOfView = fov;
        
        //vaules for the music
        musicVolume = MusicVolume.value;
        Music.volume = musicVolume;

        //score and high score system update
        ScoreText.text = "Score: " + score;
        HighScoreText.text = "High Score: " + highScore;
        FinalScore.text = "Score: " + score;

        //save and sets the highscore when the score is grater than it
        if (score > highScore)
        {
            Save_Manager.instance.activeSave.HighScore = score;

            Save_Manager.instance.Save();

            Save_Manager.instance.Load();

            highScore = Save_Manager.instance.activeSave.HighScore;
        }

        //sets the varible current health to the player's health
        currentHealth = MainScript.GetCurrentHealth();

        //checks if the player is daed to display the game over screen
        if (currentHealth <= 0)
        {
            canvas.enabled = true;
            GameOver.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            isShowing = true;
        }

        //this is very important for setting Resolution and FullScrenn settings
        FullScreen();
        Resolution();
    }

    //when called it will increass the score by y
    public void IncreassScore(int y)
    {
        score += y;
    }
    public void IncreassScore(float y)
    {
        score += (int)y;
    }

    //sets the fullscreen mode depending on the dropbox value
    public void FullScreen()
    {
        switch (VideoFullScreen.value)
        {
            case 0:
                settingVid = FullScreenMode.Windowed;
                break;

            case 1:
                settingVid = FullScreenMode.MaximizedWindow;
                break;

            case 2:
                settingVid = FullScreenMode.FullScreenWindow;
                break;

            case 3:
                settingVid = FullScreenMode.ExclusiveFullScreen;
                break;
        }
    }

    //set the resolution for the screen depending of the dropbox value.
    public void Resolution()
    {
        switch (VideoResolution.value)
        {
            case 0:
                Screen.SetResolution(4096, 2160, settingVid, 60);
                break;

            case 1:
                Screen.SetResolution(3840, 2160, settingVid, 60);
                break;

            case 2:
                Screen.SetResolution(2048, 1152, settingVid, 60);
                break;

            case 3:
                Screen.SetResolution(1920, 1080, settingVid, 60);
                break;

            case 4:
                Screen.SetResolution(1280, 720, settingVid, 60);
                break;

            case 5:
                Screen.SetResolution(640, 480, settingVid, 60);
                break;
        }
    }

    //save values after the button is clicked
    public void SaveSettings()
    {
        //save the values that are set
        Save_Manager.instance.activeSave.masterVolumeSave = masterVolume;

        Save_Manager.instance.activeSave.FullscreenMode = VideoFullScreen.value;

        Save_Manager.instance.activeSave.ScreenResolution = VideoResolution.value;

        Save_Manager.instance.activeSave.FOV = Fov.value;

        //force save
        Save_Manager.instance.Save();
    }

    //loads the settings if the settings have been changed before.
    public void LoadSettings()
    {
        //force load the current values
        Save_Manager.instance.Load();

        //save values
        masterVolume = Save_Manager.instance.activeSave.masterVolumeSave;
        MasterVolume.value = Save_Manager.instance.activeSave.masterVolumeSave;

        VideoFullScreen.value = Save_Manager.instance.activeSave.FullscreenMode;

        VideoResolution.value = Save_Manager.instance.activeSave.ScreenResolution;

        highScore = Save_Manager.instance.activeSave.HighScore;

        fov = Save_Manager.instance.activeSave.FOV;
        Fov.value = Save_Manager.instance.activeSave.FOV;
    }

    //funtion for the resume button to resume
    public void btn1_click()
    {
        isShowing = !isShowing;
        canvas.enabled = isShowing;
        mainPauseMenu.SetActive(isShowing);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //fuction for the quit button to quit
    public void btn2_click()
    {
        Application.Quit();
    }

    //funtion for the main menu button to go back to the main menu
    public void btn3_click()
    {
        Debug.Log("Loading main menu");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}

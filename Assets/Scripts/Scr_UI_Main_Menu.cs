using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;


public class Scr_UI_Main_Menu : MonoBehaviour
{
    [Header("Setting for the Settings")]
    public Text MasterVolumeText;

    public Slider MasterVolume;
    public float masterVolume;

    public Dropdown VideoResolution;
    public Dropdown VideoFullScreen;

    FullScreenMode settingVid;
    public int highScore;

    public Text HighScoreText;


    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject mapSelectionMenu;
    public GameObject settingsMenu;
    public GameObject ConfirmDeleteData;


    void Start()
    {

        //loading save values and if not default values are used instead.
        if (Save_Manager.instance.hasLoaded)
        {
            masterVolume = Save_Manager.instance.activeSave.masterVolumeSave;
            MasterVolume.value = Save_Manager.instance.activeSave.masterVolumeSave;

            VideoFullScreen.value = Save_Manager.instance.activeSave.FullscreenMode;

            VideoResolution.value = Save_Manager.instance.activeSave.ScreenResolution;

            highScore = Save_Manager.instance.activeSave.HighScore;
        }
        else
        {
            Save_Manager.instance.activeSave.masterVolumeSave = 1f;

            Save_Manager.instance.activeSave.FullscreenMode = 4;

            Save_Manager.instance.activeSave.ScreenResolution = 3;

            Save_Manager.instance.activeSave.HighScore = 0;
        }

        //menus are set so the main menu is first
        mainMenu.SetActive(true);
        mapSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        ConfirmDeleteData.SetActive(false);

        //makes the cursor visible and confines it so you can't click out
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Update()
    {
        //update values constants
        MasterVolumeText.text = "Master Volume: " + ((int)(masterVolume * 100));
        masterVolume = MasterVolume.value;
        AudioListener.volume = masterVolume;
        HighScoreText.text = "High Score: " + highScore;

        //other funtions that need to run constantly
        FullScreen();
        Resolution();
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
    
    //when called all the user's data will be whiped and reset
    public void DeleteAllUserData()
    {
        Save_Manager.instance.DeleteSaveData();

        Save_Manager.instance.activeSave.masterVolumeSave = 1f;

        Save_Manager.instance.activeSave.FullscreenMode = 4;

        Save_Manager.instance.activeSave.ScreenResolution = 3;

        Save_Manager.instance.activeSave.HighScore = 0;

        Save_Manager.instance.Save();

        LoadSettings();
    }


    //save values after the button is clicked
    public void SaveSettings()
    {
        //save the values that are set
        Save_Manager.instance.activeSave.masterVolumeSave = masterVolume;

        Save_Manager.instance.activeSave.FullscreenMode = VideoFullScreen.value;

        Save_Manager.instance.activeSave.ScreenResolution = VideoResolution.value;

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
    }

    //loads the map called Test Map 1 or Map001
    public void btn1_click()
    {
        Debug.Log("loaded Map 001");
        SceneManager.LoadScene("Map_001", LoadSceneMode.Single);
    }

    //Quits to desktop
    public void btn2_click()
    {        
        Debug.Log("Game Quitting");
        Application.Quit();
    }
    
    //loads the map called Test Map 2 or Map002
    public void btn3_click()
    {
        Debug.Log("Loaded Base Map");
        SceneManager.LoadScene("BaseMap", LoadSceneMode.Single);
    }

}

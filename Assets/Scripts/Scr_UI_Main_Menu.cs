using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;


public class Scr_UI_Main_Menu : MonoBehaviour
{
    /*
    bool mainMenuOn;
    Text Mytext;
    Button Mybutton;
    Button Mybutton2;
    Button Mybutton3;
    Canvas canvas;
    */

    Scr_Ingame_Menu gameMenu;

    [Header("Setting for the Settings")]
    public Text MasterVolumeText;

    public Slider MasterVolume;
    public float masterVolume;

    public Dropdown VideoResolution;

    FullScreenMode settingVid = FullScreenMode.MaximizedWindow;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject mapSelectionMenu;
    public GameObject settingsMenu;


    void Start()
    {
        mainMenu.SetActive(true);
        mapSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);

        //Load();


        //SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        //MainMenu();
    }

    private void Awake()
    {
        //MainMenu();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Update()
    {
        MasterVolumeText.text = "Master Volume: " + ((int)(masterVolume * 100));
        masterVolume = MasterVolume.value;
        AudioListener.volume = masterVolume;

        Resolution();
    }

    public void Resolution()
    {
        switch (VideoResolution.value)
        {
            case 0:
                Screen.SetResolution(640, 480, settingVid, 60);
                break;

            case 1:
                Screen.SetResolution(1080, 720, settingVid, 60);
                break;
        }
    }
            
      


    public void btn1_click()
    {
        Debug.Log("loaded Map 001");
        SceneManager.LoadScene("Map_001", LoadSceneMode.Single);
    }

    public void btn2_click()
    {        
        Debug.Log("Game Quitting");
        Application.Quit();
    }

    public void btn3_click()
    {
        Debug.Log("Loaded Map 002");
        SceneManager.LoadScene("Map_002", LoadSceneMode.Single);
    }

    

}

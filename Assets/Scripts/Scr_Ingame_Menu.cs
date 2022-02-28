using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_Ingame_Menu : MonoBehaviour
{
    Text Mytext;
    Button Mybutton;
    Button Mybutton2;
    Button Mybutton3;
    public Slider MasterVolume;
    public float masterVolume;
    public Text MasterVolumeText;

    public Canvas canvas;
    public GameObject mainPauseMenu;
    public GameObject comfirmQuit;
    public GameObject settingsMenu;
    Scr_UI_Main_Menu MainMenu;

    private bool isShowing = false;

    private void Start()
    {
        //MainMenu.LoadPrefs();

        //LoadSettings();
        MasterVolume.value = PlayerPrefs.GetFloat("masterVolume", 1);
        masterVolume = PlayerPrefs.GetFloat("masterVolume", 1);

        canvas.enabled = false;
        mainPauseMenu.SetActive(false);
        comfirmQuit.SetActive(false);
        settingsMenu.SetActive(false);
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

        //pause menu
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            canvas.enabled = isShowing;
            mainPauseMenu.SetActive(isShowing);
            comfirmQuit.SetActive(false);
            settingsMenu.SetActive(false);
        }
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
    }

    /*
    void MenuMake()
    {
        //Text load
        var prefabtext = Resources.Load("Text");

        GameObject Text_object = (GameObject)Instantiate(prefabtext, new Vector3(0, 0, 0), Quaternion.identity);

        Mytext = Text_object.GetComponent<Text>();

        Mytext.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mytext.alignment = TextAnchor.MiddleCenter;

        Mytext.fontSize = 20;

        Mytext.color = Color.white;

        Mytext.fontStyle = FontStyle.Bold;

        Mytext.text = "Paused";

        RectTransform Rect_text = Text_object.GetComponent<RectTransform>();

        Mytext.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.9f) - Rect_text.rect.height * 0.5f, 0);



        //Puause button
        var prefabbutton = Resources.Load("Button");

        GameObject Button_object = (GameObject)Instantiate(prefabbutton, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton = Button_object.GetComponent<Button>();

        Mybutton.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.8f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton.name = "Resume";

        Button_object.GetComponentInChildren<Text>().text = "Resume";

        //GameObject.Find("Resume").GetComponentInChildren<Text>().text = "Resume";

        Mybutton.onClick.AddListener(btn1_click);



        //Settings button
        var prefabbutton2 = Resources.Load("Button");

        GameObject Button_object2 = (GameObject)Instantiate(prefabbutton2, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton2 = Button_object2.GetComponent<Button>();

        Mybutton2.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton2.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.6f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton2.name = "Settings";

        Button_object2.GetComponentInChildren<Text>().text = "Settings";

        //GameObject.Find("Resume").GetComponentInChildren<Text>().text = "Resume";

        Mybutton2.onClick.AddListener(btn2_click);




        //Main Menu button
        var prefabbutton3 = Resources.Load("Button");

        GameObject Button_object3 = (GameObject)Instantiate(prefabbutton3, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton3 = Button_object3.GetComponent<Button>();

        Mybutton3.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton3.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.7f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton3.name = "Main Menu";

        Button_object3.GetComponentInChildren<Text>().text = "Main Menu";

        //GameObject.Find("Resume").GetComponentInChildren<Text>().text = "Resume";

        Mybutton3.onClick.AddListener(btn3_click);



        //hides canvas
        canvas.enabled = false;
    }
    */

    public void btn1_click()
    {
        isShowing = !isShowing;
        canvas.enabled = isShowing;
        mainPauseMenu.SetActive(isShowing);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void btn2_click()
    {
        //SaveSettings();
        Application.Quit();
    }

    public void btn3_click()
    {
        Debug.Log("Loading main menu");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    /*
    //saves all setting values
    public void SaveSettings()
    {
        SaveSystem.SaveSettingsToFile(this);
    }

    //loads all setting values
    public void LoadSettings()
    {
        GameSettingsData data = SaveSystem.LoadGameSettings();

        masterVolume = data.masterGameVolume;
    }
    */
}

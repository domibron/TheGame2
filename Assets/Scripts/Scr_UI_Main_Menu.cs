using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_UI_Main_Menu : MonoBehaviour
{
    bool mainMenuOn;
    Text Mytext;
    Button Mybutton;
    Button Mybutton2;
    Button Mybutton3;
    Canvas canvas;

    void Start()
    {
        //SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        //MainMenu();
    }

    private void Awake()
    {
        MainMenu();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MainMenu()
    {
        //Text load
        var prefabtext = Resources.Load("Text");

        GameObject Text_object = (GameObject)Instantiate(prefabtext, new Vector3(0, 0, 0), Quaternion.identity);

        Mytext = Text_object.GetComponent<Text>();

        Mytext.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mytext.text = "Hello World!";

        RectTransform Rect_text = Text_object.GetComponent<RectTransform>();

        Mytext.transform.Translate((Screen.width * 1.0f), (Screen.height * 1.0f) - Rect_text.rect.height * 0.5f, 0);




        //Map_001 load (Test map)
        var prefabbutton = Resources.Load("Button");

        GameObject Button_object = (GameObject)Instantiate(prefabbutton, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton = Button_object.GetComponent<Button>();

        Mybutton.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.5f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton.name = "Map_001";

        GameObject.Find("Map_001").GetComponentInChildren<Text>().text = "Test Map";

        Mybutton.onClick.AddListener(btn1_click);




        //Quit Button
        var prefabbutton2 = Resources.Load("Button");

        GameObject Button_object2 = (GameObject)Instantiate(prefabbutton2, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton2 = Button_object2.GetComponent<Button>();

        Mybutton2.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton2.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.3f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton2.name = "Quit";

        GameObject.Find("Quit").GetComponentInChildren<Text>().text = "Quit";

        Mybutton2.onClick.AddListener(btn2_click);



        //Map_002 (?)
        var prefabbutton3 = Resources.Load("Button");

        GameObject Button_object3 = (GameObject)Instantiate(prefabbutton3, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton3 = Button_object3.GetComponent<Button>();

        Mybutton3.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton3.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.4f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton3.name = "Map_002";

        GameObject.Find("Map_002").GetComponentInChildren<Text>().text = "Map_002";

        Mybutton3.onClick.AddListener(btn3_click);



        //debug message for me to know what's going on.
        Debug.Log("Loaded main menu");
    }

    void btn1_click()
    {
        Debug.Log("loaded Map 001");
        SceneManager.LoadScene("Map_001", LoadSceneMode.Single);
    }

    void btn2_click()
    {
        Debug.Log("Game Quitting");
        Application.Quit();
    }

    void btn3_click()
    {
        Debug.Log("Loaded Map 002");
        SceneManager.LoadScene("Map_002", LoadSceneMode.Single);
    }




}

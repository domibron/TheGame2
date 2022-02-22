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
    Canvas canvas;

    void Start()
    {
        //SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        MainMenu();
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


        //button load
        var prefabbutton = Resources.Load("Button");

        GameObject Button_object = (GameObject)Instantiate(prefabbutton, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton = Button_object.GetComponent<Button>();

        Mybutton.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.5f) - Rect_text.rect.height * 0.5f, 0);

        Mybutton.name = "Map_001";

        GameObject.Find("Map_001").GetComponentInChildren<Text>().text = "Map_001";

        Mybutton.onClick.AddListener(btn1_click);
    }

    void btn1_click()
    {
        SceneManager.LoadScene("Map_001", LoadSceneMode.Single);
    }




}

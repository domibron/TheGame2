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
    public Canvas canvas;
    public GameObject menu;
    private bool isShowing;

    private void Awake()
    {
        MenuMake();
        
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
        }
        if (canvas.enabled)
        {
            Time.timeScale = 0f;
        }
        else if (!canvas.enabled)
        {
            Time.timeScale = 1;
        }
    }

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

        GameObject.Find("Resume").GetComponentInChildren<Text>().text = "Resume";

        Mybutton.onClick.AddListener(btn1_click);

        //hides canvas
        canvas.enabled = false;
    }

    void btn1_click()
    {
        isShowing = !isShowing;
        canvas.enabled = isShowing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

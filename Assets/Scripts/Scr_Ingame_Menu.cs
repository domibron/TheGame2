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
    public Canvas canvas;
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

        Button_object.GetComponentInChildren<Text>().text = "Resume";

        //GameObject.Find("Resume").GetComponentInChildren<Text>().text = "Resume";

        Mybutton.onClick.AddListener(btn1_click);



        //Settings button
        var prefabbutton2 = Resources.Load("Button");

        GameObject Button_object2 = (GameObject)Instantiate(prefabbutton2, new Vector3(0, 0, 0), Quaternion.identity);

        Mybutton2 = Button_object2.GetComponent<Button>();

        Mybutton2.transform.SetParent(this.gameObject.GetComponent<RectTransform>());

        Mybutton2.transform.Translate((Screen.width * 0.5f), (Screen.height * 0.7f) - Rect_text.rect.height * 0.5f, 0);

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

    void btn1_click()
    {
        isShowing = !isShowing;
        canvas.enabled = isShowing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void btn2_click()
    {
        
    }

    void btn3_click()
    {
        Debug.Log("Loading main menu");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}

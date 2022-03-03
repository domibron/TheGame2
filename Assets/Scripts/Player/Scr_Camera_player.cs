using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Scr_Camera_player : MonoBehaviour
{
    //some canvas settings
    public Slider MouseSens;
    public Text MouseSensText;
    public Scr_Weapon_manager Weapon_Manager;

    public Camera fpsCam;
    float range = 2f;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    public float sensitivityMouse;

    float mouseX;
    float mouseY;

    float multiplier = 1f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //loading save values and if not default values are used instead.
        if (Save_Manager.instance.hasLoaded)
        {
            sensitivityMouse = Save_Manager.instance.activeSave.MainMouseSensitivity;
            MouseSens.value = Save_Manager.instance.activeSave.MainMouseSensitivity;

        }
        else
        {
            Save_Manager.instance.activeSave.MainMouseSensitivity = 1f;
        }

        LoadMouseSettings();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation += mouseX * sensitivityMouse * multiplier;
            xRotation -= mouseY * sensitivityMouse * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        MouseSensText.text = "Sensitivity: " + ((int)(MouseSens.value * 100));
        sensitivityMouse = MouseSens.value;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
            {
                if(hit.transform.tag == "Ammo")
                {
                    Weapon_Manager.AmmoFill();
                }
            }
        }
    }

    public void LoadMouseSettings()
    {
        Save_Manager.instance.Load();

        sensitivityMouse = Save_Manager.instance.activeSave.MainMouseSensitivity;
        MouseSens.value = Save_Manager.instance.activeSave.MainMouseSensitivity;
    }

    public void SaveMouseSettings()
    {
        Save_Manager.instance.activeSave.MainMouseSensitivity = sensitivityMouse;

        Save_Manager.instance.Save();
    }
}

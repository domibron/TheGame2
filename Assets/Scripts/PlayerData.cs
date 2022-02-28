using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float mainMasterVolume;

    public SettingsData (Scr_UI_Main_Menu mainMenu)
    {
        mainMasterVolume = mainMenu.masterVolume;
    }
}

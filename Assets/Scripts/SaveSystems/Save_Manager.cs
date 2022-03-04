using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Save_Manager : MonoBehaviour
{
    //this just allows any script to access this (if it's running in the scene)
    public static Save_Manager instance;
    public SaveData activeSave;

    public bool hasLoaded = false;

    private void Awake()
    {
        //when this start it will set instance but also load the data files
        instance = this;

        Load();
    }

    //when call it will find a path that will no change and create a file and save the data in save data
    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }

    //when called it will find a persistant path and look for a file with the activesave save name and load all data
    //if not it will send a error and create a new empty save file with defalt values
    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loaded" + dataPath + "/" + activeSave.saveName + ".save");
            hasLoaded = true;
        }
        else
        {
            Debug.LogError("COULD NOT FIND SAVE! on path ~ " + dataPath + "/" + activeSave.saveName + ".save | Generating new blank slate. This is not recommened!");
            hasLoaded = false;

            activeSave.masterVolumeSave = 1f;

            activeSave.FullscreenMode = 4;

            activeSave.ScreenResolution = 3;

            activeSave.HighScore = 0;

            activeSave.FOV = 100;

            activeSave.MainMouseSensitivity = 1f;

            Save();
        }
    }

    //when called it will delete the save file
    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }
}

//Save Data for saving
[System.Serializable]
public class SaveData
{
    public string saveName;

    public float masterVolumeSave;

    public int FullscreenMode;

    public int ScreenResolution;

    public int HighScore;

    public float FOV;

    public float MainMouseSensitivity;

    public float Rounds;
}

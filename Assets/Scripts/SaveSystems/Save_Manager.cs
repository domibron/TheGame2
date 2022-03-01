using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Save_Manager : MonoBehaviour
{
    public static Save_Manager instance;

    public SaveData activeSave;

    public bool hasLoaded = false;

    private void Awake()
    {
        instance = this;

        Load();
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }

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
            Debug.LogError("COULD NOT FIND SAVE! on path ~ " + dataPath + "/" + activeSave.saveName + ".save");
            hasLoaded = false;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public float masterVolumeSave;

    public int FullscreenMode;

    public int ScreenResolution;

    public int HighScore;
}

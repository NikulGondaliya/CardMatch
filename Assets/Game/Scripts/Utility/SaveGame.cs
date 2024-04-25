using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    public bool isLastDataAvailable()
    {
        return PlayerPrefs.HasKey("LastData");
    }

    string path = "Assets/Resources/Data.txt";
    public SaveData GetData()
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        SaveData data = JsonUtility.FromJson<SaveData>(reader.ReadToEnd());
        reader.Close();
        return data;
    }


    public void Save(SaveData data)
    {
        string datajson = JsonUtility.ToJson(data);
        Debug.Log("SetData = " + datajson);
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(datajson);
        writer.Close();
        PlayerPrefs.SetString("LastData", "Saved");
        ////Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load("test");
        ////Print the text from the file
        //Debug.Log(asset.text);

    }
}

[Serializable]
public class SaveData
{
    public int raw;
    public int col;
    public int score;
    public List<savecardDetail> cards = new List<savecardDetail>();

}

[Serializable]
public class savecardDetail
{
    public int type;
    public string name;
    public bool isopen;
    public bool ishide;

}




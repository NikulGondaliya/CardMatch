using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    public bool isLastDataAvailable()
    {
        return PlayerPrefs.HasKey("LastData");
    }

    public SaveData GetData()
    {
        if (PlayerPrefs.HasKey("LastData"))
        {
            Debug.Log("getData = " + PlayerPrefs.GetString("LastData"));
            SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("LastData"));
            return data;
        }
        return null;
    }


    public void Save(SaveData data)
    {
        //Debug.Log("Data = " + data.cards.Count);
        //string datajson = JsonUtility.ToJson(data);
        string datajson = JsonUtility.ToJson(data);
        Debug.Log("SetData = " + datajson);
        PlayerPrefs.SetString("LastData", datajson);
    }
}

[Serializable]
public class SaveData
{
    public int raw, col;
    public List<savecardDetail> cards;

}

[Serializable]
public class savecardDetail
{
    public int type;
    public string name;
    public bool isopen;
    public bool ishide;
    public bool isclick;

}




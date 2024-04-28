

using System.Diagnostics;

public class SaveGame : UnityEngine.MonoBehaviour
{
    string path = "Assets/Resources/Data.txt";

    public bool isLastDataAvailable()
    {
        return UnityEngine.PlayerPrefs.HasKey("LastData");
    }

    public SaveData GetData()
    {
        System.IO.StreamReader reader = new System.IO.StreamReader(path);
        SaveData data = UnityEngine.JsonUtility.FromJson<SaveData>(reader.ReadToEnd());
        //SaveData data = UnityEngine.JsonUtility.FromJson<SaveData>(UnityEngine.PlayerPrefs.GetString("LastData"));
        reader.Close();
        UnityEngine.Debug.Log(UnityEngine.PlayerPrefs.GetString("LastData"));
        return data;
    }


    public void Save(SaveData data)
    {
        string datajson = UnityEngine.JsonUtility.ToJson(data);
        //UnityEngine.Debug.Log("SetData = " + datajson);
        System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false);
        writer.WriteLine(datajson);
        writer.Close();
        UnityEngine.PlayerPrefs.SetString("LastData", datajson);
    }
}

[System.Serializable]
public class SaveData
{
    public int raw;
    public int col;
    public int score;
    public System.Collections.Generic.List<savecardDetail> cards = new System.Collections.Generic.List<savecardDetail>();

}

[System.Serializable]
public class savecardDetail
{
    public string type;
    public int no;
    public bool isopen;
    public bool ishide;

}




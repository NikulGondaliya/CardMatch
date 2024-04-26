

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
        reader.Close();
        return data;
    }


    public void Save(SaveData data)
    {
        string datajson = UnityEngine.JsonUtility.ToJson(data);
        UnityEngine.Debug.Log("SetData = " + datajson);
        System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false);
        writer.WriteLine(datajson);
        writer.Close();
        UnityEngine.PlayerPrefs.SetString("LastData", "Saved");
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
    public int type;
    public string name;
    public bool isopen;
    public bool ishide;

}




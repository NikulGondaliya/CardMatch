

using System.Diagnostics;

public class SaveGame : UnityEngine.MonoBehaviour
{
    private readonly string path = "Assets/Resources/Data.txt";

    public bool isLastDataAvailable() => UnityEngine.PlayerPrefs.HasKey("LastData");
    

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
    public SaveData() { }
    public SaveData(int raw, int col, int score , System.Collections.Generic.List<savecardDetail> cards)
    {
        this.raw = raw;
        this.col = col;
        this.score = score;
        this.cards = cards;
    }
}

[System.Serializable]
public class savecardDetail
{
    public string type;
    public int no;
    public bool isopen;
    public bool ishide;
    public savecardDetail() { }
    public savecardDetail(string cardtype,int cardNo,bool isCardopen,bool iscardHide)
    {
        type = cardtype;
        no = cardNo;
        isopen = isCardopen;
        ishide = iscardHide;
    }
}




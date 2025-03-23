using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadColor();
    }

    [System.Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData jsonData = new SaveData();
        jsonData.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(jsonData);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData jsonData = JsonUtility.FromJson<SaveData>(json);
            
            TeamColor = jsonData.TeamColor;
        }
    }
}

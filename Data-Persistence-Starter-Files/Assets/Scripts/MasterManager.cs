using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;

    public int highScore;
    public string highScoreName;
    public string playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScoreName;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.HighScore = highScore;
        data.HighScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.HighScore;
            highScoreName = data.HighScoreName;
        }
    }
}

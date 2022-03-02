using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager _instance;
    public static DataManager Instance
    {
        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = value;
                DontDestroyOnLoad(value);
            }
            else
            {
                Destroy(value);
            }
        }
    }
    #endregion

    //public string CurrentPlayerName
    //{
    //    get => LoadPlayerInfo().playerName;
    //    set
    //    {
    //        SavePlayerInfo(value, CurrentPlayerScore);
    //    }
    //}
    //public int CurrentPlayerScore
    //{
    //    get => LoadPlayerInfo().score;
    //    set
    //    {
    //        SavePlayerInfo(CurrentPlayerName, value);
    //    }
    //}

    public string currentPlayerName;
    public int highScore;

    private void Awake()
    {
        Instance = this;

        //if (GetHighScoredPlayerData() == null)
        //{
        //    SavePlayerInfo("", 0);
        //}
    }

    public void Save()
    {
        SavePlayerInfo(currentPlayerName, highScore);
    }

    public PlayerData GetHighScoredPlayerData()
    {
        return LoadPlayerInfo();
    }

    private void SavePlayerInfo(string playerName, int score)
    {
        PlayerData playerData = new PlayerData(playerName, score);

        string json = JsonUtility.ToJson(playerData);
        string dataFilePath_json = string.Format("{0}/{1}.json", Application.persistentDataPath, "my_game");

        File.WriteAllText(dataFilePath_json, json);
    }

    private PlayerData LoadPlayerInfo()
    {
        string dataFilePath_json = string.Format("{0}/{1}.json", Application.persistentDataPath, "my_game");

        if (File.Exists(dataFilePath_json))
        {
            string json = File.ReadAllText(dataFilePath_json);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            PlayerData playerData = new PlayerData("", 0);
            SavePlayerInfo(playerData.playerName, playerData.score);
            return playerData;
        }
    }

}

[Serializable]
public class PlayerData
{
    public string playerName;
    public int score;

    public PlayerData(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}


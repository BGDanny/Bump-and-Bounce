using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool isGameOver;
    public int bestScore;
    public bool powerupAvailable;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField]
    class SaveData
    {
        public int playerScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.playerScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile.json"), json);
    }

    public void LoadBestScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.playerScore;

        }
    }

    public void EvaluateBestScore(int currentScore)
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveBestScore();
        }
    }
}

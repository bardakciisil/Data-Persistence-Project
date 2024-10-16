using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
            Destroy(gameObject);

        CurrentPlayer = new Player();
        BestPlayer = LoadBestPlayer();
    }


    public Player CurrentPlayer { get; private set; }
    public Player BestPlayer { get; private set; }


    private void Update()
    {
       Debug.Log("Player name: " + CurrentPlayer.Name);
    }


    public void SetPlayerName(string name) => CurrentPlayer.Name = name;


    [System.Serializable]
    public class Player
    {
        public string Name = " ";
        public int Score = 0;
    }


    public void SaveForBestPlayer(string name,int score)
    {
        if (score < BestPlayer.Score)
            return;

        BestPlayer.Score = score;
        BestPlayer.Name = name;

        string json = JsonUtility.ToJson(BestPlayer);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public Player LoadBestPlayer()
    {
        Player bestPlayer = new Player();

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bestPlayer = JsonUtility.FromJson<Player>(json);

            return bestPlayer;
        }

        return bestPlayer;
    }
}

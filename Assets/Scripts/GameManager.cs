using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    private float score;

    [SerializeField]
    private String saveButton = "f5";
    [SerializeField]
    private String loadButton = "f9";

    public float Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public string SaveButton
    {
        get
        {
            return saveButton;
        }

        set
        {
            saveButton = value;
        }
    }

    public string LoadButton
    {
        get
        {
            return loadButton;
        }

        set
        {
            loadButton = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + Score);
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData playerData = new PlayerData();
        playerData.score = Score;

        bf.Serialize(file, playerData);
        file.Close();
    }

    private void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            Score = playerData.score;
        } else
        {
            file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            PlayerData playerData = new PlayerData();
            playerData.score = Score;

            bf.Serialize(file, playerData);
        }

        file.Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(saveButton))
        {
            Save();
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown("space"))
        {
            Score += 1;
        }

        if (Input.GetKeyDown(loadButton))
        {
            Load();
            Debug.Log("Load");
        }
            
    }
}

[Serializable]
class PlayerData
{
    public float score;
}

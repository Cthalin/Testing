using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    public float score;

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
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + score);
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData playerData = new PlayerData();
        playerData.score = score;

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
            score = playerData.score;
            Debug.Log(Application.persistentDataPath);
        } else
        {
            file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            PlayerData playerData = new PlayerData();
            playerData.score = score;

            bf.Serialize(file, playerData);
        }

        file.Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Save();
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown("b"))
        {
            score += 1;
        }

        if (Input.GetKeyDown("l"))
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

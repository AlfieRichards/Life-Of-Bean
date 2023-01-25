using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : MonoBehaviour
{
    //simple values
    public int points = 0;
    public float[] coordinates = {0f,0f};
    public List<float[]> chests = new List<float[]>();



    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    { 
        PlayerData data = SaveSystem.LoadPlayer();

        coordinates = data.coordinates;
        points = data.points;
        chests = data.chests;
    }


    private void Start() 
    {
        FirstLoad();
        Sync();
        //tells us the xyz for our range of lat and long
    }


    void Sync()
    {
        LoadPlayer();
        SavePlayer();
    }

    void FirstLoad()
    {
        string path = Application.persistentDataPath + "/player.ezeSave";
        if (File.Exists(path))
        {
            LoadPlayer();
        }
        else
        {
            SavePlayer();
        }
    }
}
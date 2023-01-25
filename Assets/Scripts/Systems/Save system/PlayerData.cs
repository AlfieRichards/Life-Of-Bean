using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    //simple values
    public float[] coordinates = {0f,0f};
    public int points = 0;
    public List<float[]> chests = new List<float[]>();


    public PlayerData (Player player)
    {
        coordinates = player.coordinates;
        points = player.points;
        chests = player.chests;
    }
}

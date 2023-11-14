using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[] level;
    public int[,]time;
    public int[]token_count;

    public GameData(Managment managment)
    {
        this.level = managment.level;
        this.time = managment.time;
        this.token_count = managment.token;
    }
}

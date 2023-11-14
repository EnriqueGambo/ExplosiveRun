using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managment : MonoBehaviour
{
    public static int total_levels = 4;
    public bool[] level = { true, false, false, false};
    public int[,] time = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
    public int[] token = {0,0,0,0};

    public void Save()
    {
        SaveGame.save(this);
    }

    public void Load()
    {
        GameData data = SaveGame.loadGame();
        level = data.level;
        time = data.time;
        token = data.token_count;
    }

    public void Update()
    {
        //currently one secne before levels.
        int current = SceneManager.GetActiveScene().buildIndex -1;
        
    }
}

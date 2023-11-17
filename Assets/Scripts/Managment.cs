using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Managment : MonoBehaviour
{
    public static int total_levels = 4;
    private bool[] level = { true, false, false, false };
    private int[,] time = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } };
    private int[] token = {0,0,0,0};

    public bool[] getlevel()
    {
        return level;
    }

    public int[,] gettime()
    {
        return time;
    }

    public int[] gettoken()
    {
        return token;
    }

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


    void Start()
    {
        Load();
    }

    public void Update()
    {
        //currently one secne before levels.
        int current = SceneManager.GetActiveScene().buildIndex -1;
     
    }

    public void ContinueLvl()
    {
        int i = 0;
        while (level[i]== true)
        {
            i++;
        }
        //Levels start at i ==one
        SceneManager.LoadSceneAsync(i);
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void updateLvlbuttons()
    {
        for(int i = 1; i <= total_levels; i++)
        {
            string name = "Level" + i;
            Debug.Log(this.level[i-1]);
            Debug.Log(name);
            Button button = GameObject.Find(name).GetComponent<Button>();
            if(button != null && this.level[i - 1] == true)
            {
                button.interactable = true;
            }else if(button != null)
            {
                button.interactable = false;
            }
        }
    }
}

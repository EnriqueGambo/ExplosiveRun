using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Managment : MonoBehaviour
{
    public static Managment Instance;
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

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        SaveGame.save(this);
    }

    public void Load()
    {
        GameData data = SaveGame.loadGame();
        if (data != null)
        {
            level = data.level;
            time = data.time;
            token = data.token_count;
            if (level[0] == false)
            {
                setNewGame();
                Save();
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
        else
        {
            setNewGame();
            Save();
            SceneManager.LoadScene(2);
        }
    }


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Load();
        }

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
        //Levels start at i ==two
        SceneManager.LoadScene(i+1);
    }

    public void NewGame()
    {
        setNewGame();
        Save();
        SceneManager.LoadSceneAsync(2);
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
            Debug.Log(level[i-1]);
            Debug.Log(name);
            Button button = GameObject.Find(name).GetComponent<Button>();
            if(button != null && level[i - 1] == true)
            {
                button.interactable = true;
            }else if(button != null)
            {
                button.interactable = false;
            }
        }
    }

    private void setNewGame()
    {
        for (int i = 0; i < total_levels ; i++)
        {
            token[i] = 0;
            time[i,0] = 0;
            time[i,1] = 0;
        }
        level[0] =true;
        for(int i = 1; i < total_levels ; i++)
        {
            level[i] = false;
        }
    }
}

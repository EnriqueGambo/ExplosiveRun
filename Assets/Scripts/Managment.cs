using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Managment : MonoBehaviour
{
    public static Managment Instance;
    public static int total_levels = 4;
    public static int secenes_before_levels = 2;
    private bool[] level = { true, false, false, false };
    private int[,] time = { { 999, 999 }, { 999, 999 }, { 999, 999 }, { 999, 999 } };
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
        GameObject gameObject =GameObject.Find("FailedLoad");
        gameObject.SetActive(false);
        GameData data = SaveGame.loadGame();
        if (data != null)
        {
            level = data.level;
            time = data.time;
            token = data.token_count;
            if (level[0] == false)
            {
                StartCoroutine(waitSec(3,gameObject));
                
            }
            else
            {
                SceneManager.LoadScene(1);


            }
        }
        else
        {
            StartCoroutine (waitSec(3, gameObject));

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
            Button button = GameObject.Find(name).GetComponent<Button>();
            if(button != null && level[i - 1] == true)
            {
                button.interactable = true;
            }else if(button != null)
            {
                button.interactable = false;
            }
            //Lvl Data
            name = "TextTime" + i;
            Text textTime = GameObject.Find(name).GetComponent<Text>();
            textTime.text = "Time: "+time[i-1,0].ToString("00") +":"+ time[i-1,1].ToString("00");
            name = "TextToken" + i;
            Text textToken = GameObject.Find(name).GetComponent<Text>();
            textToken.text = "Token: " + token[i- 1].ToString();

        }

    }

    public void gotoLvl(int lvl)
    {
        SceneManager.LoadScene(lvl + secenes_before_levels-1);
    }

    private void setNewGame()
    {
        for (int i = 0; i < total_levels ; i++)
        {
            token[i] = 0;
            time[i,0] = 999;
            time[i,1] = 999;
        }
        level[0] =true;
        for(int i = 1; i < total_levels ; i++)
        {
            level[i] = false;
        }
    }

    IEnumerator waitSec(int sec, GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        setNewGame();
        Save();
        SceneManager.LoadScene(2);
    }

    public void levelCompleted()
    {
        int current_level = SceneManager.GetActiveScene().buildIndex-1;
        LevelTimer levelTimer = GameObject.Find("TimerText (Legacy)").GetComponent<LevelTimer>();
        int timeMin = levelTimer.getMin();
        int timeSec = levelTimer.getSec();
        if (timeMin < time[current_level, 0] || (timeMin == time[current_level, 0] && timeSec < time[current_level, 1]))
        {
            time[current_level, 0] = timeMin;
            time[current_level, 1] = timeSec;
        }
        TokenCheck tokenCheck = GameObject.Find("Token1").GetComponent<TokenCheck>();
        int tokencount = tokenCheck.getToken();
        if(tokencount > token[current_level])
        {
            token[current_level] = tokencount;
        }
        Save();
       

    }
}

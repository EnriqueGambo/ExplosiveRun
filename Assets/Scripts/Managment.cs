using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
//Build not in UnityEditor 
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Managment : MonoBehaviour
{
    public static Managment Instance;
    public static int total_levels = 3;
    public static int secenes_before_levels = 2;
    private bool[] level = { true, false, false};
    private int[,] time = { { 999, 999 }, { 999, 999 }, { 999, 999 }};
    private int[] token = {0,0,0};
    private int Curr_checkpoint = 0;


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

    public int getCurr_checkpoint()
    {
        return Curr_checkpoint;
    }

    public void setCur_checkpoint(int Cur_checkpoint)
    {
        this.Curr_checkpoint = Cur_checkpoint;
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
        GameObject gameObject = GameObject.Find("FailedLoad");
        gameObject.SetActive(false);
        try
        {
            GameData data = SaveGame.loadGame();
            if (data != null)
            {
                level = data.level;
                time = data.time;
                token = data.token_count;
                if (level[0] == false)
                {
                    StartCoroutine(waitSec(2, gameObject, 2));

                }
                else
                {
                    StartCoroutine(waitSec(1, gameObject, 1));
                }
            }
            else
            {
                StartCoroutine(waitSec(2, gameObject, 0));

            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            StartCoroutine(waitSec(3, gameObject, 2));
        }
    }


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Load();
        }

    }

    public void ContinueLvl()
    {
        int i = 0;
        while (i < 3 && level[i]== true)
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
        Save();
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
        Text totalTextTime = GameObject.Find("TotalToken").GetComponent<Text>();
        int totalToken = 0;
        for(int i =0; i < total_levels; i++)
        {
            totalToken += token[i];
        }
        totalTextTime.text = "Total Tokens: " + totalToken;

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

    IEnumerator waitSec(int sec, GameObject gameObject, int status)
    {
        String open_string = "Loading .";
        Text open_text =GameObject.Find("TextOpen").GetComponent<Text>();
        yield return new WaitForSeconds(.6f);
        open_text.text = open_string;
        open_string = open_string + " .";
        //Load Failed
        if (status == 2)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(sec);
            setNewGame();
            Save();
            SceneManager.LoadScene(2);
        }
        //Load Worked
        else if (status == 1)
        {
            yield return new WaitForSeconds(.6f);
            open_text.text = open_string;
            yield return new WaitForSeconds(sec);
            SceneManager.LoadScene(1);
        }
        //If any other issue just create a new game.
        else
        {
            yield return new WaitForSeconds(sec);
            setNewGame() ;
            Save();
            SceneManager.LoadScene(2);
        }
    }


    public void levelCompleted()
    {
        
        int current_level = SceneManager.GetActiveScene().buildIndex-2;
        if(current_level+1 < 3)
        {
            level[current_level + 1] = true;
        }
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

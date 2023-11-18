using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Managment.Instance.NewGame();
    }

    public void Continue()
    {
        Managment.Instance.ContinueLvl();
    }

    public void LvlSelection()
    {
       Managment.Instance.updateLvlbuttons();
    }

    public void QuitGame()
    {
      Managment.Instance.QuitGame();
    }

    public void LvlSelected(int Lvl)
    {
        string lvlName = "Level " + Lvl;
        SceneManager.LoadScene(lvlName);
    }
}

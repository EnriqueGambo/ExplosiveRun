using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void SetGameSoundButtons()
    {
        //Setting up sound buttons.
        if (AudioListener.volume > 0)
        {
            GameObject.Find("SoundOn (Legacy)").SetActive(false);
            GameObject.Find("SoundOff (Legacy)").SetActive(true);
        }
        else
        {
            GameObject.Find("SoundOn (Legacy)").SetActive(true);
            GameObject.Find("SoundOff (Legacy)").SetActive(false);
        }
    }
}

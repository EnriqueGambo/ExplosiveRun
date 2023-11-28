using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string next_level;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player" && next_level != "end")
        {
            Managment.Instance.levelCompleted();
            Managment.Instance.setCur_checkpoint(0);
            TokenManager.instance.ResetTokens();
            SceneManager.LoadScene(next_level);
        }
        else if (next_level == "end")
        {
            Managment.Instance.levelCompleted();
            TokenManager.instance.ResetTokens();
            SceneManager.LoadSceneAsync(1);
        }
    }
}

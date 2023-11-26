using System.Collections;
using System.Collections.Generic;
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
            //Managment.Instance.levelCompleted();
            //TokenManager.instance.ResetTokens();
            SceneManager.LoadScene(next_level);
        }
    }
}

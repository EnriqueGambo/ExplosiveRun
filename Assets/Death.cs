using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Collider2D Player;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Reset();
        }
    }
    private void Reset()
    {
        SceneManager.LoadSceneAsync("ExplosiveRun");
    }
}

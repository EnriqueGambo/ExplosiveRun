using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TokenCheck : MonoBehaviour
{
    private TokenManager tokenManager; // Reference to TokenManager
    Color color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        // Find the TokenManager instance in the scene
        tokenManager = TokenManager.instance;

        // Check if TokenManager is not found
        if (tokenManager == null)
        {
            Debug.LogError("TokenManager not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // two scenes before levels
        int level = SceneManager.GetActiveScene().buildIndex - Managment.secenes_before_levels;

        // Get token count from TokenManager
        int token = tokenManager.getToken();

        // Rest of your code...
        // Update the UI based on the token count
        if (token == 0)
        {
            color.g = .25f;
            color.b = .25f;
            color.r = .25f;
            color.a = .5f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }
        else if (token == 1)
        {
            color = Color.white;
            color.a = 1f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            color.g = .25f;
            color.b = .25f;
            color.r = .25f;
            color.a = .95f;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }
        else if (token == 2)
        {
            color = Color.white;
            color.a = 1f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            color.g = .25f;
            color.b = .25f;
            color.r = .25f;
            color.a = .95f;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }
        else if (token == 3)
        {
            color = Color.white;
            color.a = 1f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }
    }

    // Add this method to get the token count
    public int getToken()
    {
        return tokenManager.getToken();
    }

    // Call this method when the player dies
    public void PlayerDied()
    {
        tokenManager.ResetTokens();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TokenCheck : MonoBehaviour
{
    int token = 0;
    Color color = Color.white;

    public int getToken()
    {
        return token;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // two secens before levels
        int level = SceneManager.GetActiveScene().buildIndex- Managment.secenes_before_levels;
        if (token == 0)
        {
            color.g = .25f;
            color.b = .25f;
            color.r = .25f;
            color.a = .5f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }else if (token == 1)
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
        }else if (token == 2)
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
        else if (token== 3)
        {
            color = Color.white;
            color.a = 1f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;

        }
    }
}

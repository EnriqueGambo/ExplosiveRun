using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TokenCheck : MonoBehaviour
{
    Color color = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int[] token = Managment.Instance.gettoken();
        // two secens before levels
        int level = SceneManager.GetActiveScene().buildIndex- Managment.secenes_before_levels;
        Debug.Log(level);
        if (token[level] == 0)
        {
            color.g = .25f;
            color.b = .25f;
            color.r = .25f;
            color.a = .5f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;
        }else if (token[level] == 1)
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
        }else if (token[level] == 2)
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
        else if (token[level] == 3)
        {
            color = Color.white;
            color.a = 1f;
            GameObject.Find("Token1").GetComponent<Image>().color = color;
            GameObject.Find("Token2").GetComponent<Image>().color = color;
            GameObject.Find("Token3").GetComponent<Image>().color = color;

        }
    }
}

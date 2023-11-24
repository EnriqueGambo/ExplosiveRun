using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokenCounter : MonoBehaviour
{
    public static TokenCounter instance;

    public TMP_Text tokenText:
    public int currentTokens = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tokenText.text = "Tokens: " + currentTokens.ToString();
    }


    public void IncreaseTokens(int v)
    {
        currentTokens += v;
        tokenText.text = "Tokens: " + currentTokens.ToString();
    }

}

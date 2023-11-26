using UnityEngine;
using TMPro;

public class TokenManager : MonoBehaviour
{
    public static TokenManager instance;

    private int tokens;
    [SerializeField] private TMP_Text tokensDisplay;

    private int startingTokens = 0; // Variable to store the initial token amount

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        tokensDisplay.text = tokens.ToString();
    }

    public void ChangeTokens(int amount)
    {
        tokens += amount;
    }

    // when the player progresses to the next level, you can set the starting tokens using:
    // TokenManager.instance.SetStartingTokens(currentTokenAmount);
    public void SetStartingTokens(int amount)
    {
        startingTokens = amount;
        ResetTokens(); // Call ResetTokens when setting starting tokens
    }

    //Gameplay code or level manager script, when the player dies, you can call:
    //TokenManager.instance.ResetTokens();
    public void ResetTokens() 
    {
        tokens = startingTokens;
    }

    public int getToken()
    {
        return tokens;
    }
}

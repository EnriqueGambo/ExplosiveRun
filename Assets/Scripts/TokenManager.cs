using UnityEngine;
using TMPro;

public class TokenManager : MonoBehaviour
{
    public static TokenManager instance;

    private int tokens;
    [SerializeField] private TMP_Text tokensDisplay;

    private int startingTokens = 0; // Variable to store the initial token amount
    private int tokensAtDeath; // New variable to store tokens at the time of death

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

    // Gameplay code or level manager script, when the player dies, you can call:
    // TokenManager.instance.PlayerDied();
    public void PlayerDied()
    {
        tokensAtDeath = tokens; // Store the current token count at the time of death
        ResetTokens();
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

    //Method to get the tokens at the time of death
    public int getTokensAtDeath()
    {
        return tokensAtDeath;
    }
}

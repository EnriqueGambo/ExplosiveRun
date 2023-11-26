
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;
    private TokenManager tokenManager;

    private void Start()
    {
        tokenManager = TokenManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            tokenManager.ChangeTokens(value);
            Destroy(gameObject);
        }
    }
}

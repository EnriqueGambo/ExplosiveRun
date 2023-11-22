using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private float power = 30f;

    [SerializeField] private Transform upcheck;
    [SerializeField] private Transform downcheck;
    [SerializeField] private Transform leftcheck;
    [SerializeField] private Transform rightcheck;
    [SerializeField] private LayerMask playLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Reset(collider);
        }
    }
    private void Reset(Collider2D Player)
    {
        movement stats = Player.GetComponent<movement>();

        if (stats.armor > 0)
            propel(Player);
        else
            SceneManager.LoadScene("ExplosiveRun");
    }
    private void propel(Collider2D Player)
    {
        movement stats = Player.GetComponent<movement>();
        Rigidbody2D force = stats.rb;

        if(Physics2D.OverlapCircle(upcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, power);
        }
        else if (Physics2D.OverlapCircle(downcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, -power);
        }
        else if (Physics2D.OverlapCircle(rightcheck.position, 0.2f, playLayer))
        {
            Debug.Log("Right");
            force.velocity = new Vector2(power, force.velocity.y);
        }
        else if (Physics2D.OverlapCircle(leftcheck.position, 0.2f, playLayer))
        {
            Debug.Log("Left");
            force.velocity = new Vector2(power, force.velocity.y);
        }
    }
}

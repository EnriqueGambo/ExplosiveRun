using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private float power = 20f;

    [SerializeField] private Transform upcheck;
    [SerializeField] private Transform downcheck;
    [SerializeField] private Transform leftcheck;
    [SerializeField] private Transform rightcheck;
    [SerializeField] private LayerMask playLayer;
    [SerializeField] private Collider2D Player;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter2D()
    {
        if (Player.gameObject.name == "Player")
        {
            Attack();
        }
    }
    private void Attack()
    {
        movement stats = Player.GetComponent<movement>();

        if (stats.armor > 0)
            propel();
        else
            SceneManager.LoadScene("ExplosiveRun");
    }
    private void propel()
    {
        movement stats = Player.GetComponent<movement>();
        Rigidbody2D force = stats.rb;

        if(Physics2D.OverlapCircle(upcheck.position, 0.2f, playLayer))
        {
            Vector2 change = new Vector2(0, power);
            force.velocity = new Vector2(force.velocity.x, power);
        }
        else if (Physics2D.OverlapCircle(downcheck.position, 0.2f, playLayer))
        {
            Vector2 change = new Vector2(0, -power);
            force.velocity = new Vector2(force.velocity.x, -power);
        }
        else if (Physics2D.OverlapCircle(rightcheck.position, 0.2f, playLayer))
        {
            Vector2 change = new Vector2(power, 0);
            force.velocity = new Vector2(power, force.velocity.y);
        }
        else if (Physics2D.OverlapCircle(leftcheck.position, 0.2f, playLayer))
        {
            Vector2 change = new Vector2(-power, 0);
            force.AddForce(change);
        }
    }
}

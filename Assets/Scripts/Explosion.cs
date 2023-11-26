using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private float power = 20f;

    [SerializeField] private Collider2D upcheck;
    [SerializeField] private Collider2D downcheck;
    [SerializeField] private Collider2D leftcheck;
    [SerializeField] private Collider2D rightcheck;
    [SerializeField] private LayerMask playLayer;

    [SerializeField] public Animator anim;
    private string[] options = {"New Animation", "Right", "Left", "Down" };
    public int choice = 0;

    public bool stays;
    // Start is called before the first frame update
    // Update is called once per frame
    private Collider2D Player;
    private void Start()
    {
        anim.Play(options[choice]);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Player = collider;
            Attack();
        }
    }

    private Stopwatch sw = Stopwatch.StartNew();
    void Update()
    {
        sw.Start();
        if(sw.ElapsedMilliseconds > 100  && stays == false) {
             Destroy(gameObject);
        }

    }
    private bool defended = false;
    private void Attack()
    {
        movement stats = Player.GetComponent<movement>();

        if (stats.armor > 0)
        {
            stats.armor--;
            propel();
            defended = true;
        }
        else if(!defended)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void propel()
    {
        movement stats = Player.GetComponent<movement>();
        Rigidbody2D force = stats.rb;

        if(Physics2D.IsTouchingLayers(upcheck, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, -power);
        }
        else if (Physics2D.IsTouchingLayers(downcheck, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, power*1.4f);
            Player.GetComponent<movement>().jump_count--;
        }
        else if (Physics2D.IsTouchingLayers(rightcheck, playLayer))
        {
            force.velocity = new Vector2(-power, force.velocity.y);
            stats.stopfall();
        }
        else if (Physics2D.IsTouchingLayers(leftcheck, playLayer))
        {
            force.velocity = new Vector2(power, force.velocity.y);
            stats.stopfall();
        }
    }
}

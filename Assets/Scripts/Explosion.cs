using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private float power = 20f;

    [SerializeField] private Transform upcheck;
    [SerializeField] private Transform downcheck;
    [SerializeField] private Transform leftcheck;
    [SerializeField] private Transform rightcheck;
    [SerializeField] private LayerMask playLayer;
    [SerializeField] public Collider2D Player;
    [SerializeField] public Animator anim;
    private string[] options = {"New Animation", "Right", "Left", "Down" };
    public int choice = 0;

    public bool stays;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Start()
    {
        anim.Play(options[choice]);
    }
    void OnTriggerEnter2D()
    {
        if (Player.gameObject.name == "Player")
        {
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

        if(Physics2D.OverlapCircle(upcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, -power);
        }
        else if (Physics2D.OverlapCircle(downcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(force.velocity.x, power*1.4f);
            Player.GetComponent<movement>().jump_count--;
        }
        else if (Physics2D.OverlapCircle(rightcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(-power, force.velocity.y);
        }
        else if (Physics2D.OverlapCircle(leftcheck.position, 0.2f, playLayer))
        {
            force.velocity = new Vector2(power, force.velocity.y);
        }
    }
}

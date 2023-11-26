using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class TileAI : MonoBehaviour
{
    [SerializeField] private Collider2D event_collider;
    [SerializeField] private Collider2D player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject boom;
    [SerializeField] private GameObject side_boom;
    [SerializeField] private Collider2D[] checks;
    private bool has_touched = false;
    public bool[] powers = new bool[3];
    private Stopwatch sw = new Stopwatch();
    // Start is called before the first frame update

    // Update is called once per frame
    private int dir = 0;

    private bool has_got = false;
    void Update()
    {
        if (Contact())
        {
            if (!has_got)
            {
                dir = direction();
                has_got = true;
            }
            sw.Start();
            if (sw.Elapsed.Milliseconds > 500)
                explode();
            has_touched = true;

        }
        else if (has_touched == true)
            explode();
    }

    private Stopwatch bomb_timer = new Stopwatch();
    private float change = .8f;
    private bool set_off = false;
    private void explode()
    {
        bomb_timer.Start();
        if (set_off) { }
        else if (dir == 1)
        {
            Instantiate(side_boom, new Vector2(transform.position.x - change, transform.position.y), new Quaternion(0, 0, 1, 0));
            set_off = true;
        }
        else if (dir == 2)
        {
            Instantiate(side_boom, new Vector2(transform.position.x + change, transform.position.y), new Quaternion(0, 0, 0, 0));
            set_off = true;
        }
        else if (dir == 3)
        {
            Instantiate(boom, new Vector2(transform.position.x, transform.position.y + change), new Quaternion(0, 0, 1, 0));
            set_off = true;
        }
        else
        {
            Instantiate(boom, new Vector2(transform.position.x, transform.position.y - change), new Quaternion(0, 0, 0, 0));
            set_off = true;

        }

        if (bomb_timer.ElapsedMilliseconds < 200)
            return;
        if (powers[0])
            player.GetComponent<movement>().armor++;
        if (powers[1])
            player.GetComponent<movement>().speed*=(float)1.5;
        if (powers[2])
            player.GetComponent<movement>().jump_count++;
        Explosion exp = explosion.GetComponent<Explosion>();
        exp.choice = dir;
        exp.stays = false;

        Quaternion rot = new Quaternion(0, 0, 180, 0);
        Instantiate(explosion, transform.position, rot);
        

        Destroy(gameObject);
    }
    private bool Contact()
    {
        return event_collider.IsTouching(player);
    }

    private int direction()
    {
        for(int i = 0; i < checks.Length; i++)
            if (checks[i].IsTouching(player))
                return i;
        return 0;
    }
}

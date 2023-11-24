using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
public class TileAI : MonoBehaviour
{
    [SerializeField] private Collider2D event_collider;
    [SerializeField] private Collider2D player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Collider2D[] checks;
    private bool has_touched = false;
    private Stopwatch sw = new Stopwatch();
    private Stopwatch anim = new Stopwatch();

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

    private void explode()
    {
        anim.Start();

        Explosion exp = explosion.GetComponent<Explosion>();
        exp.Player = player;
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
        if (checks[0].IsTouching(player))
            return 0;
        else if (checks[1].IsTouching(player))
            return 1;
        else if (checks[2].IsTouching(player))
            return 2;
        if (checks[3].IsTouching(player))
            return 3;
        return 0;
    }
}

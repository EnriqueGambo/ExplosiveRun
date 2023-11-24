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
    Armor armor = new Armor();
    SpeedBoost speedboost = new SpeedBoost();
    DoubleJump doublejump = new DoubleJump();
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
    private void explode()
    {
        bomb_timer.Start();

        if (bomb_timer.ElapsedMilliseconds < 150)
            return;
        Explosion exp = explosion.GetComponent<Explosion>();
        exp.Player = player;
        exp.choice = dir;
        exp.stays = false;

        Quaternion rot = new Quaternion(0, 0, 180, 0);
        Instantiate(explosion, transform.position, rot);
        
        if(event_collider.gameObject.name == "ArmorTile")
            armor.OnTriggerEnter2D(player);
        else if(event_collider.gameObject.name == "SpeedBoostTile")
            speedboost.OnTriggerEnter2D(player);
        else if(event_collider.gameObject.name == "DoubleJumpTile")
            doublejump.OnTriggerEnter2D(player);

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

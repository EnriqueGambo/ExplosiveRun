using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
public class TileAI : MonoBehaviour
{
    [SerializeField] private Collider2D event_collider;
    [SerializeField] private Collider2D player;
    private bool has_touched = false;
    private Stopwatch sw = new Stopwatch();

    // Start is called before the first frame update

    // Update is called once per frame

    void Update()
    {
        if (Contact())
        {
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
        Destroy(gameObject);
    }
    private bool Contact()
    {
        return event_collider.IsTouching(player);
    }
}

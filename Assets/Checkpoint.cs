using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform playCheck;
    [SerializeField] private LayerMask playLayer;
    [SerializeField] private Collider2D Player;

    private bool has_touched = false;

    // Start is called before the first frame update

    // Update is called once per frame

    void Update()
    {
        if (Contact())
        {
            movement stats = Player.GetComponent<movement>();
            stats.Spawn = new Vector2 (transform.position.x, transform.position.y+1);
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
        return Physics2D.OverlapCircle(playCheck.position, 0.2f, playLayer);
    }
}

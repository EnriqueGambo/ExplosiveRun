using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform playCheck;
    [SerializeField] private LayerMask playLayer;
    [SerializeField] private Collider2D Player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject boom;

    public SpriteRenderer render;
    public Sprite newSprite;
    public bool is_start;
    public int checkpoint_num;

    private bool has_touched = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (checkpoint_num == Managment.Instance.getCurr_checkpoint())
        {
            movement stats = Player.GetComponent<movement>();
            stats.pos = transform.position;
            stats.started = true;
        }

    }
    // Update is called once per frame

    void Update()
    {
        if (Contact())
        {
            Managment.Instance.setCur_checkpoint(checkpoint_num);
            has_touched = true;
            changeCheckpointSprite();
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
        exp.choice = 0;
        exp.stays = false;

        Quaternion rot = new Quaternion(0, 0, 180, 0);
        Instantiate(explosion, transform.position, rot);
        Instantiate(boom, transform.position, new Quaternion(0, 0, 0, 0));

        Destroy(gameObject);
    }
    private bool Contact()
    {
        return Physics2D.OverlapCircle(playCheck.position, 0.2f, playLayer);
    }
    void changeCheckpointSprite()
    {
        render.sprite = newSprite;
    }
}

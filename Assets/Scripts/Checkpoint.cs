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

    public string spawn_file;
    private bool will_restart = false;

    private bool has_touched = false;

    // Start is called before the first frame update
    private void Start()
    {
        if(is_start)
        {
            StreamReader sr = new StreamReader("Assets/Scripts/Data/" + spawn_file);
            sr.ReadLine();
            sr.ReadLine();
            if (int.Parse(sr.ReadLine()) == 1)
            {
                will_restart = true;
                UnityEngine.Debug.Log("It activates");
            }
            sr.Close();
        }

        if (will_restart)
        {
            StreamWriter sw = new StreamWriter("Assets/Scripts/Data/" + spawn_file);
            float y_level = transform.position.y + 2;
            string data = transform.position.x.ToString() + "\n" + y_level.ToString() + "\n0";
            sw.WriteLine(data);
            sw.Close();
            StartCoroutine(moveplayer(.5f));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
    // Update is called once per frame

    void Update()
    {
        if (Contact())
        {
            StreamWriter sw = new StreamWriter("Assets/Scripts/Data/" + spawn_file);
            float y_level = transform.position.y + 2;
            string data = transform.position.x.ToString() + "\n" + y_level.ToString() + "\n0";
            sw.WriteLine(data);
            sw.Close();

            movement stats = Player.GetComponent<movement>();
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
    private IEnumerator moveplayer(float seconds)
    {
        GameObject obj = Player.GetComponent<GameObject>();
        yield return new WaitForSeconds(seconds);
        UnityEngine.Debug.Log("Why");
        obj.transform.position = new Vector2(transform.position.x-30, transform.position.y + 2);
    }
}

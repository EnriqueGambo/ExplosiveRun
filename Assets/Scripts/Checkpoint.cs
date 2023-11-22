using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform playCheck;
    [SerializeField] private LayerMask playLayer;
    [SerializeField] private Collider2D Player;

    public string spawn_file;

    private bool has_touched = false;

    // Start is called before the first frame update

    // Update is called once per frame

    void Update()
    {
        if (Contact())
        {
            StreamWriter sw = new StreamWriter("Assets/Scripts/Data/" + spawn_file);
            float y_level = transform.position.y + 2;
            string data = transform.position.x.ToString() + "\n" + y_level.ToString();
            sw.WriteLine(data);
            sw.Close();

            movement stats = Player.GetComponent<movement>();
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

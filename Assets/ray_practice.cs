using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ray_practice : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask ground;
    private Ray ray;
    void Start()
    {
        Vector2 checkpos = transform.position-new Vector3(0f, transform.position.y/2);
        RaycastHit2D hit = Physics2D.Raycast(checkpos, Vector3.down);

        if (hit)
        {
            Debug.Log("It works");
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 checkpos = transform.position - new Vector3(0f, transform.position.y / 2);
        RaycastHit2D hit = Physics2D.Raycast(checkpos, Vector3.down);

        if (hit)
        {
            Debug.Log("It works");
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }
}

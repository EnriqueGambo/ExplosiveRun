using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Collider : MonoBehaviour
{
    private string[] viable_tiles = { "Grass", "CheckPoint", "Platform-groundlayer"};
    private Collider2D sol;
    void OnTriggerEnter2D(Collider2D collider)
    {
        foreach(string s in viable_tiles)
        {
            if(collider.gameObject.name == s)
            {
                sol = collider;
                break;
            }
        }
    }
    public Collider2D answer()
    {
        return sol;
    }
}

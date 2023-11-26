using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Collider : MonoBehaviour
{
    private string[] viable_tiles = { "Grass", "CheckPoint", "Platform-groundlayer"};
    private Collider2D sol;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
            sol = collider;
    }
    public bool answer()
    {
        if(sol != null)
            return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public static void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            Pickup(collider);
        }
    }
    static void Pickup(Collider2D Player)
    {
        movement stats =  Player.GetComponent<movement>();
        stats.armor++;
    }
}

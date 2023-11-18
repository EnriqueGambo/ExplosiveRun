using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            Pickup(collider);
        }
    }
    void Pickup(Collider2D Player)
    {
        Debug.Log("Double Jump Acquired");
        
        Destroy(gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>() != null)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if(player.transform.position.x > transform.position.x)
                player.Knockback(1);
            else if (player.transform.position.x < transform.position.x)
                player.Knockback(-1);
            else
            {
                player.Knockback(0);
            }
            
        }
    }
}

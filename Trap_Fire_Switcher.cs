using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{
    public Fire_Trap myTrap;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>() != null)
        {
            myTrap.FireSwitchAfter(5);
            anim.SetTrigger("Pressed");
        }
    }
}

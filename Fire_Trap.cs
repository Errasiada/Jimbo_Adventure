using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Trap : Trap
{
    public bool isWorking;
    public bool hasSwitcher;
    private Animator anim;
    public float repeatRate;
    private void Start()
    {
        anim = GetComponent<Animator>();
        
        if(!hasSwitcher)
            InvokeRepeating("FireSwitch",0, repeatRate);
    }

    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (isWorking)
        {
            base.OnTriggerEnter2D(col);
        }
    }

    public void FireSwitchAfter(float seconds)
    {
        FireSwitch();
        Invoke("FireSwitch", seconds);
    }

    public void FireSwitch()
    {
        isWorking = !isWorking;
    }
    
}

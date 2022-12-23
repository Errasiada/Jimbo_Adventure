using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField] private bool isWorking;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] float speed;
    private int movePointIndex;
    private float cooldownTimer;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        anim.SetBool("isWorking", isWorking);
        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            movePointIndex++;

            if (movePointIndex >= movePoint.Length)
            {
                movePointIndex = 0;
            }
            
        }
    }
}

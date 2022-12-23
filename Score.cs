using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public TextMeshProUGUI MyscoreText;
    private int ScoreNum;
    
    void Start()
    {
        ScoreNum = 0;
        MyscoreText.text = "Bananas: " + ScoreNum;
    }

    private void OnTriggerEnter2D(Collider2D Fruit)
    {
        if (Fruit.tag == "Fruit")
        {
            ScoreNum += 1;
            MyscoreText.text = "Bananas: " + ScoreNum;
        }
    }

    private void OnTriggerExit2D(Collider2D Trap)
    {
        if (Trap.tag == "Trap")
        {
            MyscoreText.text = "Bananas: " +- ScoreNum;
        }
    }
}

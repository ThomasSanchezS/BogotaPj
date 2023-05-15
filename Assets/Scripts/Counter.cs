using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    public TMP_Text texto;
    public int cards;

    void Start()
    {
        cards = 0;
        texto.text = "Tarjetas: " + " 0/4" ;
    }

    public void UpdateCounter(){
        cards++;
        texto.text = "Tarjetas: " + cards.ToString() + "/4";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
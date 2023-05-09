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
        texto.text = "Monedas:" + " 0" ;
    }

    public void UpdateCounter(){
        cards++;
        texto.text = "Monedas " + cards.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
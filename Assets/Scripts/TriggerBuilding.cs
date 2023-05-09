using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBuilding : MonoBehaviour
{
    //public static int cards = 0;
    public Counter counter;

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && counter.cards == 4){
            SceneManager.LoadScene("Nivel3");
        }
    }
}

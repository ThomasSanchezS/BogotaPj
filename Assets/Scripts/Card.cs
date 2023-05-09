using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public static int cards = 0;
    public Counter counter;

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            cards++;
            counter.UpdateCounter();
            gameObject.SetActive(false);
        }
    }
}
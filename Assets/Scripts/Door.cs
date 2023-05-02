using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool HasKey = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){

        if(HasKey && other.CompareTag("Player")){
            OpenDoor();
        }
    }

    private void OpenDoor(){

        gameObject.SetActive(false);
    }
}

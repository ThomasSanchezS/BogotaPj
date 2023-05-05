using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool HasKey = false;
    

     private void OnTriggerEnter(Collider other){

        if(HasKey == true && other.CompareTag("Player")){
            Debug.Log("Trigger");
            OpenDoor();
        }
    }


    private void OpenDoor(){
        this.transform.parent.gameObject.SetActive(false);
    }
}

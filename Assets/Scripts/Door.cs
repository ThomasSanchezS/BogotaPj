using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool HasKey = false;
    

     private void OnTriggerEnter(Collider other){

        if(HasKey == true && other.CompareTag("Player")){
            SceneManager.LoadScene("Nivel2");
            OpenDoor();
        }
    }


    private void OpenDoor(){
        this.transform.parent.gameObject.SetActive(false);
    }
}

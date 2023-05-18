using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animate;
    public GameObject openText;

    

    public bool inReach;
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        inReach = false;
    }

      void OnCollisionEnter(Collision other){
        Debug.Log("colision");
        if (other.gameObject.name == "gordoF") {
            animate.SetBool("Open", true);
    }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Reach") {
            inReach = true;
            openText.SetActive(true);
        }
        if (other.gameObject.name == "gordoF") {
            animate.SetBool("Open", true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Reach") {
            DoorOpens();
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetButtonDown("Interact")){
            if(!isOpened){
                DoorOpens();
                Debug.Log("se abrió esta sapa hpta");
            }
            else{
                DoorCloses();
           }
            
        }
    }

    void DoorOpens(){
        animate.SetBool("Open", true);
        animate.SetBool("Closed", false);
        isOpened = true;
    }

    void DoorCloses(){
        Debug.Log("se cerró esta sapa hpta");
        animate.SetBool("Open", false);
        animate.SetBool("Closed", true);
        isOpened = false;
    }
}

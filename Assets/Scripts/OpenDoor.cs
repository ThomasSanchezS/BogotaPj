using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animate;
    public GameObject openText;

    public AudioSource doorSound;

    public bool inReach;
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        inReach = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Reach") {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Reach") {
            inReach = false;
            openText.SetActive(false);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetButtonDown("Interact")){

            DoorOpens();
        }
        else{
            DoorCloses();
        }
    }

    void DoorOpens(){
        Debug.Log("se abrió esta sapa hpta");
        animate.SetBool("Open", true);
        animate.SetBool("Closed", false);
        doorSound.Play();
        isOpened = true;
    }

    void DoorCloses(){
        Debug.Log("se cerró esta sapa hpta");
        animate.SetBool("Open", false);
        animate.SetBool("Closed", true);
        doorSound.Stop();
    }
}

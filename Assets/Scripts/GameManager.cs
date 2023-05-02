using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float restartDelay=1f;
    public GameObject[] playerCharacters;
    public GameManager gameManager;

    private void Start(){
         playerCharacters = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Nivel2(){
        SceneManager.LoadScene("Nivel2");
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			gameManager.Nivel2();
		}
	}
}


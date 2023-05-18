using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Start is called before the first frame update
 public void PlayGame(){
    SceneManager.LoadScene("SampleScene");
    }


public void QuitGame () {
    Debug.Log("Te saliste puto");
    //Application.Quit();
    }

public void ResetLVL2(){
    SceneManager.LoadScene("Nivel2");
    }

public void Menu(){
    SceneManager.LoadScene("Menu");
    }

public void ResetLVL3(){
    SceneManager.LoadScene("Nivel3");
    }
}
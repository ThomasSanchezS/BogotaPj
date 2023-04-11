using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    public float energyAmount = 25f; // la cantidad de energía que se recuperará
    public float rotationSpeed = 100f; // la velocidad de rotación del objeto

    void Update()
    {
        // hacemos que el objeto rote constantemente
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // detectamos si el jugador ha entrado en contacto con el objeto
        if (other.CompareTag("Player"))
        {
            // recuperamos la barra de energía del jugador
            EnergyBar energyBar = other.GetComponent<EnergyBar>();
            if (energyBar != null)
            {
                energyBar.AddEnergy(energyAmount);
                Debug.Log(energyAmount);
                Destroy(gameObject);
            }
        }
    }
}
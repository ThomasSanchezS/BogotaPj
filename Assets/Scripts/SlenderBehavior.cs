using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SlenderBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float detectionDistance = 20f;
    public float teleportDistance = 2f;
    public float teleportCooldown = 5f;
    public float minTimeBetweenTeleports = 10f;
    public float maxTimeBetweenTeleports = 20f;
    public float minDistanceFromP = 10f;
    public float maxDistanceFromP = 20f;
    public Animator animate;
    

    private Transform player;
    private CharacterController controller;
    private float teleportTimer;
    private float timeBetweenTeleports;
    //private float angleTimer;
    private bool isTeleporting = false;
    //private bool isChasing = false;
    public EnergyBar energyBar;
    public float currentEnergy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
        timeBetweenTeleports = Random.Range(minTimeBetweenTeleports, maxTimeBetweenTeleports);
        teleportTimer = timeBetweenTeleports;
        //angleTimer = 5f;
        energyBar = player.gameObject.GetComponent<EnergyBar>();
        currentEnergy = energyBar.energy;
        animate = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isTeleporting)
        {

            if (Vector3.Distance(transform.position, player.position) >= detectionDistance)
            {
                //isChasing = false;
            }
            // Si el jugador está lo suficientemente cerca, persigue al jugador
            if (Vector3.Distance(transform.position, player.position) <= detectionDistance)
            {
                //angleTimer = 5f;
                Vector3 playerPos = player.position;
                playerPos.y = 0;
                transform.LookAt(playerPos);
                controller.Move(transform.forward * moveSpeed * Time.deltaTime);
                animate.SetBool("isChasing", true);
                //isChasing = true;
                if (Vector3.Distance(transform.position, player.position) <= teleportDistance)
                {
                    //StartCoroutine(Teleport());
                }
            }else{
                animate.SetBool("isChasing", false);
            }
            // Si no, elige una nueva dirección al azar y camina
            /**else if (!isChasing)
            {
                if (angleTimer <= 0)
                {
                    transform.Rotate(0, transform.localEulerAngles.y + Random.Range(-10, 10), 0);
                    angleTimer = 5f;
                }
                angleTimer -= Time.deltaTime;
                controller.Move(transform.forward * moveSpeed * Time.deltaTime);
            }**/
        }

        // Actualiza el temporizador de teletransporte
        teleportTimer -= Time.deltaTime;

        // Si el temporizador de teletransporte llega a cero, teletransporta al enemigo a una ubicación aleatoria
        if (teleportTimer <= 0 && !isTeleporting)
        {
            StartCoroutine(Teleport());
        }
        if(transform.position.y != 0){
            Vector3 posY = transform.position;
            posY.y = 0;
            transform.position = posY;
        }
    }

    IEnumerator Teleport()
    {
        isTeleporting = true;
        //isChasing = false;
        controller.enabled = false;
        yield return new WaitForSeconds(1f);
        transform.position = GetRandomPosition();
        controller.enabled = true;
        teleportTimer = timeBetweenTeleports;
        if(energyBar.energy <= 50){
            teleportTimer *= 0.25f;
        }
        if(energyBar.energy <= 50){
            timeBetweenTeleports = Random.Range(minTimeBetweenTeleports / 2, maxTimeBetweenTeleports / 2);
        }else{
            timeBetweenTeleports = Random.Range(minTimeBetweenTeleports, maxTimeBetweenTeleports);
        }
        isTeleporting = false;
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);
        float[] opciones = new float[4];
        int randomNum = Random.Range(0, 2);
        int randomNumz = Random.Range(2,4);
        
        if(energyBar.energy <= 50){
            
            opciones[0] = Random.Range(player.position.x + minDistanceFromP, player.position.x + maxDistanceFromP);
            opciones[2] = Random.Range(player.position.z + minDistanceFromP, player.position.z + maxDistanceFromP);
            opciones[1] = Random.Range(player.position.x - minDistanceFromP, player.position.x - maxDistanceFromP);
            opciones[3] = Random.Range(player.position.z - minDistanceFromP, player.position.z - maxDistanceFromP);
            x = opciones[randomNum];
            z = opciones[randomNumz];
        }
        if(energyBar.energy <= 10){
            opciones[0] = Random.Range(player.position.x + minDistanceFromP / 2, player.position.x + maxDistanceFromP / 2);
            opciones[2] = Random.Range(player.position.z + minDistanceFromP / 2, player.position.z + maxDistanceFromP / 2);
            opciones[1] = Random.Range(player.position.x - minDistanceFromP / 2, player.position.x - maxDistanceFromP / 2);
            opciones[3] = Random.Range(player.position.z - minDistanceFromP / 2, player.position.z - maxDistanceFromP / 2);
            x = opciones[randomNum];
            z = opciones[randomNumz];
        }
        return new Vector3(x, 1, z);
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Jugador") {
             SceneManager.LoadScene("GameOver1");
		}
	}
}
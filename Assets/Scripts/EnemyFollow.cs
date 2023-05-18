using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed;
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop;

    public Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;
    public Transform player;
    public Transform[] wayPoints;
    public int currentWayPoint;
    public float verticalDistance;
    public Animator animate;
    

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        currentWayPoint = 0;
        animate.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = player.transform.position;
        verticalDistance = Mathf.Abs(targetPoint.y - transform.position.y);
        if(!chasing){
            if(Vector3.Distance(transform.position, targetPoint)< distanceToChase && verticalDistance <= 3){
                chasing = true;
            }

            targetPoint = wayPoints[currentWayPoint].position;
            Vector2 horizontalPos = new Vector2(targetPoint.x, targetPoint.z);
            Vector2 horizontalAgent = new Vector2(transform.position.x, transform.position.z);
            agent.destination = targetPoint;
            if(Vector2.Distance(horizontalPos, horizontalAgent) <= 2f && Mathf.Abs(transform.position.y - targetPoint.y) <= 3f){
                Debug.Log("Ya llegue lampara");
                currentWayPoint++;
                currentWayPoint = currentWayPoint % wayPoints.Length;
            }

            if(chaseCounter > 0){
                chaseCounter -= Time.deltaTime;
                if(chaseCounter <= 0){
                    agent.destination = startPoint;
                }
            }
        }
        else{
            //transform.LookAt(targetPoint);

            //theRB.velocity = transform.forward * moveSpeed;

            agent.destination = targetPoint;

            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose){
                chasing = false;
                chaseCounter = keepChasingTime;
            }
        }
    }

     void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
            SceneManager.LoadScene("GameOver2");
		}
    }
}

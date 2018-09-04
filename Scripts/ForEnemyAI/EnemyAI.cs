using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	
	public float playerDistance;
	public float rotationDamp;
	GameObject GO;
	public float damage = 5f;
	Animator anim;

	public static bool isPlayerAlive = true;

	//for patrolling
	public enum State {
		PATROL, LOOKCHASE
	}

	public State state;
	public NavMeshAgent navAgent;

	private int waypointIndex;
	public float patrolSpeed = 0.5f;

	//for chasing
	public float chaseSpeed = 1.0f;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

		GO = GameObject.FindGameObjectWithTag("spec");
		
		navAgent = GetComponent<NavMeshAgent> ();

		navAgent.updatePosition = true;
		navAgent.updateRotation = true;

		GO.GetComponent<wayPointScript>().waypoints = GameObject.FindGameObjectsWithTag("GuardPoint");
		waypointIndex = Random.Range (0, GO.GetComponent<wayPointScript>().waypoints.Length);
			
		state = EnemyAI.State.PATROL;

		StartCoroutine ("FSM");
	}

	IEnumerator FSM ()
	{
		while (isPlayerAlive) {

			switch (state) {

			case State.PATROL:
				patrol();
				break;

			case State.LOOKCHASE:
				if (isPlayerAlive == true) {
					lookAtPlayer ();
					chase ();
				}
				break;

			  }
			yield return null;
			}
			
		}


	void FixedUpdate ()
	{		
	
		if (playerDistance <= 25f) {

			anim.SetBool ("isShooting", true);
			attack ();

		} else {
		
			anim.SetBool ("isShooting", false);
			transform.GetComponent<NavMeshAgent> ().Resume();
		}
		if (isPlayerAlive == true) {

			playerDistance = Vector3.Distance (GO.GetComponent<wayPointScript>().player.transform.position, transform.position);

			if (playerDistance > 45f) {
				anim.SetBool ("isWalking", true);
				state = EnemyAI.State.PATROL;
			} else {
				anim.SetBool ("isWalking", false);
			}

			if (playerDistance <= 45f && playerDistance > 25f) {
				anim.SetBool ("isChasing", true);
				state = EnemyAI.State.LOOKCHASE;
			} else {
				anim.SetBool ("isChasing", false);
			}

		
		}
	}

	void lookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation (GO.GetComponent<wayPointScript>().player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDamp);

	}

	void chase()
	{
		navAgent.speed = chaseSpeed;
		navAgent.SetDestination (GO.GetComponent<wayPointScript>().player.transform.position);

	}

	void attack()
	{
		transform.GetComponent<NavMeshAgent> ().Stop ();

		RaycastHit hit;
		if (Physics.Raycast (this.transform.position,transform.forward, out hit)) 
		{
			if (hit.collider.gameObject.tag == "Player") 
			{
				hit.collider.gameObject.GetComponent<PlayerHealth>().currentHealth -= damage; 
				GO.GetComponent<wayPointScript> ().player.GetComponent<HealthBar> ().UpdateHealthBar ();
			}

		}	 
	}	
		
		
	void patrol()
	{

		navAgent.speed = patrolSpeed;
		if (Vector3.Distance (this.transform.position, GO.GetComponent<wayPointScript>().waypoints [waypointIndex].transform.position) >= 2) 
		{
			navAgent.SetDestination (GO.GetComponent<wayPointScript>().waypoints [waypointIndex].transform.position);

		}else if (Vector3.Distance (this.transform.position, GO.GetComponent<wayPointScript>().waypoints [waypointIndex].transform.position) <= 2) 
		{
			waypointIndex = Random.Range (0, GO.GetComponent<wayPointScript>().waypoints.Length);
		}
	}



		
}
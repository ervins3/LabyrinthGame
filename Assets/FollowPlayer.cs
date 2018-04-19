using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FollowPlayer : MonoBehaviour {

	private bool isHit = false;
	[Range(0f, 10f)]
	[SerializeField]
	private float closeEnoughDistance = 1f;

	[Range(0f, 5f)]
	[SerializeField]
	private float attackRadius = 3f;

	private Animator animator;

	private GameObject player;
	private NavMeshAgent Agent;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
		Agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

	}


	void Update()
	{

		if (Vector3.Distance (transform.position, player.transform.position) < closeEnoughDistance) { // check if distance between player and gameobject is greater than close enough value

			PerformFollowPlayer ();
			animator.SetBool("Idle", false);

			if (Vector3.Distance (transform.position, player.transform.position) < attackRadius) {

				animator.SetBool ("isNearPlayer", false);
				animator.SetBool ("Attack", true);

			} else {

				animator.SetBool ("isNearPlayer", true);
				animator.SetBool ("Attack", false);
			}

		} else {
			
			if (isHit == false) {
				
				animator.SetBool("isNearPlayer", false);
				animator.SetBool("Attack", false);
				animator.SetBool("Idle", true);

			} else {
				
				animator.SetBool("Idle", false);
				animator.SetBool("isNearPlayer", true);
			}

		}
	}

	/// <summary>
	/// Follows Player
	/// </summary>
	/// 
	private void PerformFollowPlayer()
	{ 	

		Agent.SetDestination(player.transform.position);
		animator.SetBool ("isNearPlayer", true);
	}

	public void StopFollow()
	{
		Debug.Log(" Stop ");
		Agent.isStopped = true;
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, closeEnoughDistance);

	}

}




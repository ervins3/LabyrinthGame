using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class FollowPlayer : MonoBehaviour {

	[Range(0f, 10f)]
	[SerializeField]
	private float closeEnoughDistance = 1f;

	private GameObject player;
	private NavMeshAgent Agent;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
		Agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{

		if (Vector3.Distance(transform.position, player.transform.position) < closeEnoughDistance) // check if distance between player and gameobject is greater than close enough value
		{
			PerformFollowPlayer();
		}
	}

	/// <summary>
	/// Follows Player
	/// </summary>
	/// 
	private void PerformFollowPlayer()
	{
		Agent.SetDestination(player.transform.position);
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, closeEnoughDistance);
	}

}




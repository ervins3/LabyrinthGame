
using UnityEngine;

public class Target : MonoBehaviour {

	public float health = 50f;
	public float damage = 10f;

	private AudioSource audioSource;
	public AudioClip zombieDieSound;
	public AudioClip playerHurtSound;

	public GameObject zombie;
	//public GameObject hand;

	private Animator animator;

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	//public int attackDamage = 10;               // The amount of health taken away per attack.

	Health playerHealth;
	GameObject player;
	bool playerInRange;
	float timer;

	void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <Health> ();
	}

	public void TakeDamage (float amount){ //enemy damage
		health -= amount; 
		if (health <= 0f) {
			Die ();
		}
	}	
	/*
	void OnCollisionEnter(Collision other)  //if enemy touches player, reduce health
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Health> ().TakeDamage (damage);
			audioSource.PlayOneShot(playerHurtSound);
			Debug.Log ("pieskaros");
		}
	}
*/

	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject.tag == "Player")
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject.tag == "Player")
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			// ... attack.
			Attack ();
		}
			
	}

	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		// If the player has health to lose...
		if (playerHealth.currentHealth > 0) {
			// ... damage the player.
			playerHealth.TakeDamage (damage);
			audioSource.PlayOneShot (playerHurtSound);
		} else {
			Debug.Log ("ataks");
			FindObjectOfType<GameManager> ().EndGame ();
		}
	}

	void Die (){

		playerInRange = false;
		GetComponent<FollowPlayer> ().StopFollow ();
		animator.SetTrigger ("Death");
		audioSource.PlayOneShot(zombieDieSound);
		SphereCollider[] myColliders = zombie.GetComponents<SphereCollider>();
		foreach (SphereCollider bc in myColliders) bc.enabled = false;
		CapsuleCollider[] S = zombie.GetComponents<CapsuleCollider>();
		foreach (CapsuleCollider cc in S) cc.enabled = false;
		Destroy (gameObject, 3f);
	}
}

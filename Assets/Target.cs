
using UnityEngine;

public class Target : MonoBehaviour {

	public float health = 50f;
	public float damage = 10f;

	public void TakeDamage (float amount){ //enemy damage
		health -= amount; 
		if (health <= 0f) {
			Die ();
		}
	}	

	void OnCollisionEnter(Collision other)  //if enemy touches player, reduce health
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Health> ().TakeDamage (damage);
			Debug.Log ("pieskaros");
		}
	}
		
	void Die (){
		Destroy (gameObject);
	}
}

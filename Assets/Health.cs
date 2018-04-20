using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public Slider HealthBar;
	public float maxHealth = 100f;
	public float currentHealth = 100f;
	public bool alive = true;

	//GameManager gameManager;

	// Use this for initialization
	void Start () {

		currentHealth = GlobalController.Instance.currentHealth;
		HealthBar.value = currentHealth;
		alive = true;
		//currentHealth = maxHealth;
	}


	public void SavePlayer()
	{
		GlobalController.Instance.currentHealth = currentHealth;
	}


	public void TakeDamage(float amount) //player health reducing
	{

		if(!alive)
		{
			//FindObjectOfType<GameManager> ().EndGame ();
			return;
		}

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			alive = false;



		}

		currentHealth -= amount;
		HealthBar.value = currentHealth;
		Debug.Log("trapits  " + currentHealth);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLeve : MonoBehaviour {

	Scene sceneLoaded;

	Health health;
	Gun gun;
	GameObject player;
	GameObject playerGun;
	//public int sceneNr = 0;

	void Start(){
		sceneLoaded=SceneManager.GetActiveScene();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerGun = GameObject.FindGameObjectWithTag ("Weapon");
		health = player.GetComponent<Health> ();
		gun = playerGun.GetComponent<Gun> ();
	}

	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player") {
			if (sceneLoaded.buildIndex == 1) {
				//SceneManager.LoadScene (2);
				//GlobalController.Instance.currentHealth = health.currentHealth;
				//GlobalController.Instance.currentAmmo = gun.currentAmmo;
				SavePlayer();
				StartCoroutine(FadeToNext(2));


			}
			if(sceneLoaded.buildIndex == 2){
				//SceneManager.LoadScene (0);
				StartCoroutine(FadeToNext(0));
			}
		}
	}

	public void SavePlayer()
	{
		GlobalController.Instance.currentHealth = health.currentHealth;
		GlobalController.Instance.currentAmmo = gun.currentAmmo;

	}

	IEnumerator FadeToNext(int sceneNr)
	{
		float fadeTime = GameObject.FindGameObjectWithTag("Player").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene (sceneNr);
	}
}

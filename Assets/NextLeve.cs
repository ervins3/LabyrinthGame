using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLeve : MonoBehaviour {

	Scene sceneLoaded;

	void Start(){
		sceneLoaded=SceneManager.GetActiveScene();
	}

	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player") {
			if (sceneLoaded.buildIndex == 1) {
				SceneManager.LoadScene (2);

			}
			if(sceneLoaded.buildIndex == 2){
				SceneManager.LoadScene (0);

			}
		}
	}
}

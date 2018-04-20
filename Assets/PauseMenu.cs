using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;
	public Transform Player;

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (gameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void Resume(){

		Screen.lockCursor = true;
		Player.GetComponent<FirstPersonController> ().enabled = true;
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause(){

		Screen.lockCursor = false;
		Player.GetComponent<FirstPersonController> ().enabled = false;
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void LoadMenu(){
		Time.timeScale = 1f;
		gameIsPaused = false;
		SceneManager.LoadScene (0);
	}

	public void ExitGame(){
		Debug.Log ("exit");
		Application.Quit ();
	}
}

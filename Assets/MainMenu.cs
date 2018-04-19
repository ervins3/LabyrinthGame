using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start(){
		Screen.lockCursor = false;
	}

	public void PlayGame(){

		SceneManager.LoadScene (1);

	}

	public void ExitGame(){
		Debug.Log ("QUIT!");
		Application.Quit ();
	}
}

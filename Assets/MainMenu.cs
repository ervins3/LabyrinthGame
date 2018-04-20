using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start(){
		Screen.lockCursor = false;
	}

	public void PlayGame(){

		StartCoroutine (FadeToNext());

	}

	IEnumerator FadeToNext()
	{
		float fadeTime = GameObject.Find("Canvas").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene (1);
	}

	public void ExitGame(){
		Debug.Log ("QUIT!");
		Application.Quit ();
	}
}

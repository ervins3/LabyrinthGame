using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;

	public float restartDelay = 1f;

	/*public GameObject completeLevelUI;

	public void CompleteLevel ()
	{
		completeLevelUI.SetActive(true);
	}*/

	public void EndGame ()
	{
		Debug.Log ("Seitttt");
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			Debug.Log("GAME OVER");
			//Invoke("Restart", restartDelay);
			StartCoroutine(FadeToNext());
		}
	}

	void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	IEnumerator FadeToNext()
	{
		float fadeTime = GameObject.FindGameObjectWithTag("Player").GetComponent<FadingToDie>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene (0);
	}

}
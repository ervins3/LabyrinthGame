using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

	public static GlobalController Instance;

	public float currentHealth;
	public int currentAmmo;
	//public int totalAmmo;

	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
	/*
	public void SavePlayer()
	{
		GlobalController.Instance.currentHealth = currentHealth;
		GlobalController.Instance.currentAmmo = currentAmmo;

	}*/
}

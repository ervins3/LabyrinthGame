using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	public int totalAmmo = 50;
	public int currentAmmo = 20;
	public int ammoPerMag = 20;
	public float reloadTime = 1f;
	private bool isReloading = false;

	public Animator animator;

	public Text ammoText;

	private AudioSource audioSource;
	public AudioClip shootSound;
	public AudioClip emptyShotSound;
	public AudioClip reloadSound;
	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

	//public GameObject pauseMenuUI;

	void start(){
		//pauseMenuUI = GameObject.FindGameObjectWithTag ("PlayerUI");
		currentAmmo = GlobalController.Instance.currentAmmo;
		updateAmmoText ();
	}


	public void SavePlayer()
	{
		GlobalController.Instance.currentAmmo = currentAmmo;
	}


	void OnEnable(){
		isReloading = false;
		animator.SetBool ("Reloading", false);
	}
	
	// Update is called once per frame
	void Update () {

		if (isReloading) {
			return;
		}

		if (currentAmmo <= 0) {
			StartCoroutine(Reload());
			return;
		}
			
		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
			//if(pauseMenuUI.GetComponent<>){
			//	return;
		//	}
			audioSource = GetComponent<AudioSource>();
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			if (currentAmmo < ammoPerMag && totalAmmo > 0) {
				StartCoroutine(Reload());
			}
			
		}
		
	}

	IEnumerator Reload(){

		if (totalAmmo <= 0 || currentAmmo == ammoPerMag) {
			if(totalAmmo <= 0 && Input.GetMouseButtonDown(0)){
				PlayEmptySound ();
				yield break;
			}
			yield break;
		}
		PlayReloadSound ();
		isReloading = true;
		Debug.Log ("Reloading...");

		animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime - .25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (.25f);

		int bulletsToLoad = ammoPerMag - currentAmmo;
		int bulletsToDeduct = (totalAmmo >= bulletsToLoad) ? bulletsToLoad : totalAmmo;

		totalAmmo -= bulletsToDeduct;
		currentAmmo += bulletsToDeduct;

		isReloading = false;
		updateAmmoText();

	}

	void Shoot (){

		PlayShootSound();
		muzzleFlash.Play();
		RaycastHit hit;

		currentAmmo--; 
		updateAmmoText(); // update ui ammo text

		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {

				Debug.Log (hit.transform.name);

				Target target = hit.transform.GetComponent<Target> ();  //damage enemy
				if (target != null) {
					target.TakeDamage (damage);
				}
				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce (-hit.normal * impactForce);
				}
				GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (impactGO, 2f);	

		}
		
	}
	private void PlayShootSound()
	{
		audioSource.PlayOneShot(shootSound);
	}

	private void PlayReloadSound(){
		audioSource.PlayOneShot (reloadSound);
	}

	private void PlayEmptySound(){
		audioSource.PlayOneShot (emptyShotSound);
	}

	private void updateAmmoText()
	{
		ammoText.text = currentAmmo + " / " + ammoPerMag + "    " + totalAmmo;
	}
}

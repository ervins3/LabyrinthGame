using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	public int maxAmmo = 30;
	private int currentAmmo;
	public float reloadTime = 1f;
	private bool isReloading = false;

	public Animator animator;

	public Text ammoText;

	private AudioSource audioSource;
	public AudioClip shootSound;
	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

	void start(){
		
		currentAmmo = maxAmmo;
			
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
			audioSource = GetComponent<AudioSource>();
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}

		/*if (Input.GetKeyDown("r")) {
			Reload ();
			Debug.Log ("nospiests r");
		}*/
		
	}

	IEnumerator Reload(){

		isReloading = true;
		Debug.Log ("Reloading...");

		animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime - .25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (.25f);

		currentAmmo = maxAmmo;
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

				Target target = hit.transform.GetComponent<Target> ();
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

	private void updateAmmoText()
	{
		ammoText.text = currentAmmo + " / " + maxAmmo;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoca : MonoBehaviour {

	public Vector3[] position;

	public Transform FirePoint;
	public GameObject bulletPrefab;
	private Animator anim;
	public AudioClip Crachat;

	public void SpawnCola () {
		int randomNumber = Random.Range (0, position.Length);
		transform.position = position [randomNumber];
	}

	public void Shoot(){
		Instantiate (bulletPrefab, FirePoint.position, FirePoint.rotation);
	}

	void Start () {
		anim = GetComponent<Animator> ();
		StartCoroutine(SpawnColaAndWait());
	}

	void Update () {
		if (PlayerPrefs.GetInt("Explose") == 0) {	
			StartCoroutine (SpawnColaAndWait ());
		}
	}
		
	IEnumerator SpawnColaAndWait(){
		SpawnCola ();
		yield return new WaitForSeconds (2f);
		Shoot ();
		anim.SetBool ("isShooting", true);
		GetComponent<AudioSource> ().PlayOneShot (Crachat);
		yield return new WaitForSeconds (1f);
		anim.SetBool ("isShooting", false);
		yield return new WaitForSeconds (1f);
		StartCoroutine (SpawnColaAndWait ());

	}
}

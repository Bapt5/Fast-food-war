using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNavet : MonoBehaviour {

	private Vector3 pointSpawn;
	public AudioClip Croque;
	public GameObject Player;
	private int tutorialTime;

	void Start () {
		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		if (Vector3.Distance(gameObject.transform.position,Player.transform.position)<=3){
			Start ();
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Debug.Log (tutorialTime);
		if (PlayerPrefs.GetInt ("Tutorial") == 0) {
				tutorialTime = tutorialTime + 1;
				if (tutorialTime==3) {
					PlayerPrefs.SetInt ("tutorialStep", 1);
				}
				if (tutorialTime==5) {
					PlayerPrefs.SetInt ("tutorialStep", 2);
				}
				if (tutorialTime==8) {
					PlayerPrefs.SetInt ("tutorialStep", 3);
				}
		}
			GetComponent<AudioSource> ().PlayOneShot (Croque);
			Spawn ();
		}
	}

	void Spawn () {
		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		if (Vector3.Distance(gameObject.transform.position,Player.transform.position)<=3){
			Spawn ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour {

	public float speed = 1f;
	private Transform target;
	public GameObject Player;
	public GameObject GameOver;
	public GameObject Scene;
	public GameObject Joystick;
	public GameObject Pause;
	private Vector3 pointSpawn;
	private int ScorePlayer;
	private int AugmenteSpeed;
	private int BestScorePlayer;
	private int HealthPlayer;
	public GameObject KetchupPrefab;
	public Transform FirePoint;
	private float TimerSpawn;
	private GameObject Prank;
	private int isPrancking;


	IEnumerator SpawnKetchup(){
		for (int i = 0; i < 99999; i++) {
			TimerSpawn = Random.Range (8.0f, 13.0f);
			yield return new WaitForSeconds (TimerSpawn);
			Instantiate (KetchupPrefab, FirePoint.position, FirePoint.rotation);
			yield return new WaitForSeconds (0f);
		}
	}
		
	void Start () {
		target = Player.transform;
		GameOver.SetActive (false);
		Scene.SetActive (true);
		StartCoroutine (SpawnKetchup ());

		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		AugmenteSpeed = 5;
		speed = 1f;
	}

	void Update () {
		isPrancking = PlayerPrefs.GetInt ("isPranking");
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);	

		ScorePlayer = PlayerPrefs.GetInt("Score");
		BestScorePlayer = PlayerPrefs.GetInt("bestScore");
		HealthPlayer = PlayerPrefs.GetInt("Health");

		if (ScorePlayer > AugmenteSpeed) {
			speed = speed + 0.2f;
			AugmenteSpeed = AugmenteSpeed + 5;
		}

		if (HealthPlayer == 0) {
			GameOver.SetActive (true);
			Scene.SetActive (false);
		}
		if (isPrancking == 1) {	
			Prank = GameObject.FindGameObjectWithTag ("Prank");
			target = Prank.transform;
		}
		if (isPrancking == 0) {	
			target = Player.transform;
		}

		if (PlayerPrefs.GetInt ("Explose") == 0) {	
			StartCoroutine (SpawnKetchup ());
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (PlayerPrefs.GetInt ("Health") <= 3) {
				GameOver.SetActive (true);
				Scene.SetActive (false);
				Joystick.SetActive (false);
				Pause.SetActive (false);
			}
			if (PlayerPrefs.GetInt ("Health") > 3) {
				PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health")-3);
				PlayerPrefs.SetInt ("ReSpawn", 1);
			}
		}
	}
}

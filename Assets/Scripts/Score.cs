using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int ScorePlayer = 0;
	private int bestScore;
	public Text textScore;
	public AudioClip Dégat;
	public int Bonus = 0;
	private bool BonusOK = false;
	public GameObject Space;
	public GameObject Prank;
	public Transform PrankSpawn;
	public GameObject Particles;
	public GameObject Burger;
	public GameObject Coca;
	public GameObject BurgerCopy;
	public GameObject CocaCopy;
	public Transform SpawnParticule;
	public Color SpeedColor;
	public Color Normal;

	IEnumerator SpawnPrank(){
		Transform t = gameObject.GetComponent <Transform> ();
		PrankSpawn.position = t.position;
		Instantiate (Prank, PrankSpawn.position, PrankSpawn.rotation);
		PlayerPrefs.SetInt ("isPranking", 1);
		yield return new WaitForSeconds (12f);
		PlayerPrefs.SetInt ("isPranking", 0);
		yield return new WaitForSeconds (0.1f);
		Destroy (GameObject.FindGameObjectWithTag("Prank"));
	}

	IEnumerator Explosion(){
		PlayerPrefs.SetInt ("Explose", 1);
		Instantiate (Particles,SpawnParticule.position,SpawnParticule.rotation);
		Burger.SetActive (false);
		Coca.SetActive (false);
		BurgerCopy.SetActive (true);
		CocaCopy.SetActive (true);
		yield return new WaitForSeconds (8f);
		Burger.SetActive (true);
		Coca.SetActive (true);
		BurgerCopy.SetActive (false);
		CocaCopy.SetActive (false);
		PlayerPrefs.SetInt ("Explose", 0);
		yield return new WaitForSeconds (0.00001f);
		PlayerPrefs.SetInt ("Explose", 1);
	}

	IEnumerator Speed(){
		SpriteRenderer s = GetComponent<SpriteRenderer> ();
		s.color = SpeedColor;
		PlayerPrefs.SetFloat ("SpeedPlayer", 4);
		yield return new WaitForSeconds (7f);
		PlayerPrefs.SetFloat ("SpeedPlayer", 3);
		s.color = Normal;
		if (PlayerPrefs.GetInt ("Tutorial") == 0) {
			PlayerPrefs.SetInt ("tutorialStep", 5);
		}
	}

	public void Power () {
		if(BonusOK==true&&PlayerPrefs.GetInt("BonusValue")==3){
			BonusOK=false;
			Space.SetActive (false);
			Bonus=Bonus-10;
			StartCoroutine (SpawnPrank ());
		}

		if(BonusOK==true&&PlayerPrefs.GetInt("BonusValue")==2){
			BonusOK=false;
			Space.SetActive (false);
			Bonus=Bonus-10;
			StartCoroutine (Explosion ());
		}

		if(BonusOK==true&&PlayerPrefs.GetInt("BonusValue")==1){
			BonusOK=false;
			Space.SetActive (false);
			Bonus=Bonus-10;
			StartCoroutine (Speed());
		}
	
	}


	void Start () {
		PlayerPrefs.SetInt ("Explose", 1);
		textScore.text = "Score : " + ScorePlayer;
		Space.SetActive (false);
		PlayerPrefs.SetInt ("isPranking", 0);
	}

	void Update (){

		CocaCopy.transform.position = Coca.transform.position;
		BurgerCopy.transform.position = Burger.transform.position;

		bestScore = PlayerPrefs.GetInt ("bestScore");
		PlayerPrefs.SetInt ("Score", ScorePlayer);
		if (ScorePlayer > bestScore) {
			PlayerPrefs.SetInt ("bestScore", ScorePlayer);
		}

		if (Bonus >= 10&&PlayerPrefs.GetInt("BonusValue")!=0) {
			Space.SetActive (true);
			BonusOK = true;
		}

		if (BonusOK == true) {
			Space.SetActive (true);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.SetInt ("bestScore", 0);
			Debug.Log ("reset");
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			Bonus = 10;
			Debug.Log ("test");
		}

		if (PlayerPrefs.GetInt ("PouvoirTutorial") == 1) {
			Bonus = 10;
			PlayerPrefs.SetInt ("BonusValue", 1);
			PlayerPrefs.SetInt ("PouvoirTutorial", 0);
		}

		if (Input.GetKeyDown (KeyCode.Y)) {
			PlayerPrefs.SetInt("Health", 6);
			Debug.Log ("testHealth");
		}
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Navet") {
			ScorePlayer = ScorePlayer + 1;
			Bonus = Bonus + 1;
			textScore.text = "Score : " + ScorePlayer;
		}
		if (col.gameObject.tag == "bullet") {
			GetComponent<AudioSource> ().PlayOneShot (Dégat);
		}
	}
}

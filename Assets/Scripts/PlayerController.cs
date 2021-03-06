using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]private bl_Joystick Joystick;

    private Rigidbody2D rb;

    public float speed;
	private bool facingRight = true;
	private Vector3 pointSpawn;
	private Animator anim;
	public AudioSource Pas;
	public Color Ketchup;
	public Color Normal;
	public Color Coca;
	public Color Respawn_Color;
	public AudioClip ReSpawn_Sound;
	public AudioClip Glisse;
	private float v; 
	private float h;
	public GameObject Burger; 

	IEnumerator TouchKetchup(){
		SpriteRenderer s = GetComponent<SpriteRenderer> ();
		PlayerPrefs.SetFloat ("SpeedPlayer", 1.5f);
		s.color = Ketchup;
		GetComponent<AudioSource> ().PlayOneShot (Glisse);
		yield return new WaitForSeconds (8f);
		PlayerPrefs.SetFloat ("SpeedPlayer", 3f);
		s.color = Normal;
	}

	IEnumerator TouchCoca(){
		SpriteRenderer s = GetComponent<SpriteRenderer> ();
		s.color = Coca;
		yield return new WaitForSeconds (0.5f);
		s.color = Normal;
	}

	IEnumerator ReSpawn(){
		GetComponent <AudioSource> ().PlayOneShot (ReSpawn_Sound);
		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		if (Vector3.Distance(gameObject.transform.position,Burger.transform.position)<=5){
			StartCoroutine (ReSpawn ());
		}
		SpriteRenderer s = GetComponent<SpriteRenderer> ();
		s.color = Respawn_Color;
		yield return new WaitForSeconds (2f);
		s.color = Normal;

	}
		
	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		PlayerPrefs.SetFloat ("SpeedPlayer", 3f);

		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		if (Vector3.Distance(gameObject.transform.position,Burger.transform.position)<=5){
			Start ();
		}

		anim = GetComponent<Animator> ();
    }

    private void Update()
    {
		PlayerPrefs.SetInt ("Menu", 0);

		v = Joystick.Vertical;
		h = Joystick.Horizontal;

		speed = PlayerPrefs.GetFloat ("SpeedPlayer");

		//faire avancer le personnage en fonction de move v et de h
		rb.velocity = new Vector2 (v * speed/7, rb.velocity.y);


		rb.velocity = new Vector2 (h * speed/7, rb.velocity.x);

		if (v == 0 && h == 0) {
			anim.SetBool ("isWalking", false);
			Pas.volume = 0f;
		}

		if (v != 0 || h != 0) {
			anim.SetBool ("isWalking", true);
			Pas.volume = 0.5f;
		}

		if (PlayerPrefs.GetInt ("ReSpawn")==1) {
			PlayerPrefs.SetInt ("ReSpawn", 0);
			StartCoroutine (ReSpawn ());
		}
    }


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ketchup") {
			StartCoroutine (TouchKetchup ());
		}

		if (col.gameObject.tag == "bullet") {
			StartCoroutine (TouchCoca ());
		}
	}

    private void FixedUpdate(){
		//Retourner le joueur quand il le faut du bon coté
		if (facingRight == false && h < 0) {
			Flip ();
		} else if (facingRight == true && h > 0) {
			Flip ();
		}
	}


	//Retourner le joueur
	private void Flip () 
	{

		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;

	}
}

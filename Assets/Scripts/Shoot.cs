using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public float speed = -8f;
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health")-1);
		}
		Destroy (gameObject);
	}

}

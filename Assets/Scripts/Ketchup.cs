using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketchup : MonoBehaviour {

	IEnumerator DestroyKetchup(){
			yield return new WaitForSeconds (15f);
			Destroy (gameObject);
	}

	void Start () {
		StartCoroutine(DestroyKetchup());	
	}

	void OnCollisionEnter2D (Collision2D col) {
		Destroy (gameObject);
	}
}

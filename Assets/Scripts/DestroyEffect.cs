using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

	IEnumerator Destroy(){
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Destroy ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

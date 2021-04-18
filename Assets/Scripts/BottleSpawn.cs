using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawn : MonoBehaviour {

	private Vector3 pointSpawn;
	public GameObject bottlePrefab;
	public GameObject Player;

	IEnumerator SpawnBottle(){
		yield return new WaitForSeconds (Random.Range (20.0f, 30.0f));
		int xAxe = Random.Range (-8,8);
		int yAxe = Random.Range (-4,4);
		pointSpawn.x = xAxe;
		pointSpawn.y = yAxe;
		transform.position = pointSpawn;
		if (Vector3.Distance(gameObject.transform.position,Player.transform.position)<=1){
			StartCoroutine (SpawnBottle ());
		}
		Instantiate (bottlePrefab, transform.position, transform.rotation);
		StartCoroutine (SpawnBottle ());
	}

	void Start () {
		StartCoroutine (SpawnBottle ());
	}

}

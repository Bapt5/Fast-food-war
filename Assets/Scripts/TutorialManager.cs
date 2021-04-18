using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	public GameObject[] popUps;
	private int popUpIndex = 0;
	public GameObject Burger;
	public GameObject Coca;
	public GameObject Bottle;
	public GameObject Health;

	void Awake(){
		if (PlayerPrefs.GetInt ("Tutorial") == 1) {
			Application.LoadLevel ("Menu");
		}
	}

	void Start (){
		popUpIndex = 0;
		PlayerPrefs.SetInt ("TutorialStep", 1);
		PlayerPrefs.SetInt ("Tutorial", 0);
		Burger.SetActive (false);
		Coca.SetActive (false);
		Bottle.SetActive (false);
		Health.SetActive (false);
	}

	void Update () {

		for (int i = 0; i < popUps.Length; i++) {
			if (i == popUpIndex) {
				popUps [popUpIndex].SetActive (true);
			} else {
				popUps [popUpIndex].SetActive (false);
			}
		}

		if (popUpIndex == 0) {
			if (PlayerPrefs.GetInt ("tutorialStep") == 1){
				popUpIndex = popUpIndex + 1;
				Burger.SetActive (true);
				Health.SetActive (true);
			}
		}else if (popUpIndex == 1) {
			if (PlayerPrefs.GetInt ("tutorialStep") == 2){
				popUpIndex = popUpIndex + 1;
				Coca.SetActive (true);
			}
		}else if (popUpIndex == 2) {
			if (PlayerPrefs.GetInt ("tutorialStep") == 3){
				popUpIndex = popUpIndex + 1;
				Bottle.SetActive (true);
			}
		}else if (popUpIndex == 3) {
			if (PlayerPrefs.GetInt ("tutorialStep") == 4){
				popUpIndex = popUpIndex + 1;
				PlayerPrefs.SetInt ("PouvoirTutorial", 1);
			}
		}else if (popUpIndex == 4) {
			if (PlayerPrefs.GetInt ("tutorialStep") == 5){
				Application.LoadLevel ("Menu");
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	private bool GamePause=false;
	public GameObject Pause;

	public void ButtonPause(){
		GamePause = !GamePause;
	}

	public void Resume (){
		GamePause = false;
	}

	void Update () {
		if (GamePause == true) {
			Time.timeScale=0;
			Pause.SetActive (true);
		}

		if (GamePause == false) {
			Time.timeScale=1;
			Pause.SetActive (false);
		}
	}
}

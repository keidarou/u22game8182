//buttonとかbutton1とかよくわかんにゃい
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonForAuto : MonoBehaviour {

	public GameObject gameobj;
	public GameObject pause;
	public GameObject restart;
	public GameObject quit;

	movetheballautomatic script;

	public void ButtonClick() {
		switch (transform.name) {
		case "Pause":
			gameobj.GetComponent<movetheballautomatic>().pauseflag = true;
			break;
		case "Restart":
			script.pauseflag = false;
			script.restartflag = true;
			break;
		case "Quit":
			SceneManager.LoadScene ("StageSelect");
			break;
		default:
			break;
		}
	}

	// Use this for initialization
	void Start () {
		script = gameobj.GetComponent<movetheballautomatic>(); 
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(script.pauseflag);
		if (script.pauseflag == false) {
			restart.SetActive (false);
			quit.SetActive (false);
			pause.SetActive (true);
		} else {
			restart.SetActive (true);
			quit.SetActive (true);
			pause.SetActive (false);

		}

	}

}

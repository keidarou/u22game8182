using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveScene : UIBehaviour {

	string[] mapCode={
		"",//ここはなにもかかない
		"1dayo",//stage1
		"",
		"",
		"",
		"",//stage5
		"",
		"",
		"",
		"",
		"",//stage10
		"",
		"",
		"",
		"",
		"15dayo",//stage15
	};
	public int stgNum;


	// Use this for initialization
	//protected override void Start () {

		//base.Start ();

		//GetComponent<Button> ().onClick.AddListener (OnClick);
	//}
	public void OnClick(){
		Debug.Log ("de");
		SceneManager.LoadScene ("MainGame");

	}

}

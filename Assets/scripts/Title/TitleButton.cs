//タイトルからボタンタップでstageSelectシーンにきりかえる
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]


public class TitleButton : UIBehaviour {

	// Use this for initialization
	protected override void Start () {

		base.Start ();

		//GetComponent<Button> ().onClick.AddListener (OnClick);
	}


	void OnClick(){

		SceneManager.LoadScene ("StageSelect");

	}


	// Update is called once per frame
	void Update () {
		
	}
}

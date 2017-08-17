using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateButton : MonoBehaviour {

	public int page;
	public GameObject stageButton;

	public int startY,startX;

	public float spaceY,spaceX;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 5; j++) {
				GameObject button = (GameObject)Instantiate (stageButton) as GameObject;
				button.GetComponent<MoveScene> ().stgNum = i * 5 + j + 1;
				button.transform.parent = this.transform;
				button.transform.position = new Vector3 (spaceX * j + startX, spaceY * (-i) + startY, 0);

				Text text = button.GetComponentInChildren<Text> ();
				text.text = string.Format ("{0}{1}", "STAGE", Convert.ToString (i * 5 + j + 1));

			}
		}
	}

}
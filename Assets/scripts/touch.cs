using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour {

	public int ret;

	private float minSwipeDistX;
	private float minSwipeDistY;
	Vector2 endPos,startPos;
	float swipeDistX,swipeDistY;
	float SignValueX,SignValueY;//方向取得のための符号
	void Start () {
		minSwipeDistX = 30;//スワイプ判定最小値
		minSwipeDistY = 30;//スワイプ判定最小値
		Debug.Log ("left");
	}

	public int GetTouch(){
		return ret;
	}

	void Update () {
		ret = 0;

		if (Input.GetKeyDown(KeyCode.A)) { ret = 1; }
		if (Input.GetKeyDown(KeyCode.D)) { ret = 2; }
		if (Input.GetKeyDown(KeyCode.W)) { ret = 4; }
		if (Input.GetKeyDown(KeyCode.S)) { ret = 3; }
		if (Input.GetKeyDown(KeyCode.Q)) { ret = -1; }


		if (Input.touchCount > 0) {

			//タッチじょうほう取得
			Touch touch = Input.touches [0];

			switch (touch.phase) {//touch開始

			case TouchPhase.Began://とりまstartPos
				startPos = touch.position;
				break;

			case TouchPhase.Ended://touch終了
				endPos = new Vector2 (touch.position.x, touch.position.y);
				swipeDistX = (new Vector3 (endPos.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;//vectorの長さ
				swipeDistY = (new Vector3 (0, endPos.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;

				if (swipeDistX > swipeDistY && swipeDistX > minSwipeDistX) {//スワイプ距離
					SignValueX = Mathf.Sign (endPos.x - startPos.x);//符号とり
					if (SignValueX > 0) {
						ret = 1;
						Debug.Log ("right");//
					} else if (SignValueX < 0) {
						//ひだりにスワイプ
						Debug.Log ("left");
						ret = 2;
					}
				} else if (swipeDistY > minSwipeDistY) {//スワイプ距離
					SignValueY = Mathf.Sign (endPos.y - startPos.y);//符号とり
					if (SignValueY > 0) {
						Debug.Log ("うえ");
						ret = 3;
						//うえにスワイプ(うえでゆびをはなしてる)

					} else if (SignValueY < 0) {
						Debug.Log ("した");
						ret = 4;
						//したにスワイプ
					}
				}
				if (swipeDistX < minSwipeDistX && swipeDistY < minSwipeDistY) {
					Debug.Log ("tap");
					ret = -1;
					// けっきょくタップ
				}
				break;
			}
		}
	}
}

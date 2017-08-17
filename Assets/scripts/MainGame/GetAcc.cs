//端末の加速度から
//あとで実機テストします
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAcc : MonoBehaviour
{

	//-----8/17 2:50　追加分
	public bool ctrlCfg = true;//操作設定:0で画面回転入力,1でフリック
	public GameObject touch; 
	touch touchScript;
	int lastTouch;

    private Vector3 acc;//端末の加速度
                        //フォントでばっぐよう
    private GUIStyle labelStyle;
    //関数の返り値
    public int ret;

    // Use this for initialization
    void Start()
    {
        //フォントさくせい
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 20;
        this.labelStyle.normal.textColor = Color.blue;
		touchScript = touch.GetComponent<touch> ();

    }

    // Update is called once per frame
    void Update()
    {
        this.acc = Input.acceleration;
        //画面の対角線を境にして重力による加速度のベクトルで判断
        float ru = Mathf.Atan2(1.0f, 1.0f); //Mathf.Atan2(Screen.height / 2.0f, Screen.width / 2.0f);
        float lu = Mathf.Atan2(1.0f, -1.0f); //Mathf.Atan2(Screen.height / 2.0f, -Screen.width / 2.0f);
        float ld = Mathf.Atan2(-1.0f, -1.0f); //Mathf.Atan2(-Screen.height / 2.0f, -Screen.width / 2.0f);
        float rd = Mathf.Atan2(-1.0f, 1.0f); //Mathf.Atan2(-Screen.height / 2.0f, Screen.width / 2.0f);

        float d = Mathf.Atan2(acc.y, acc.x);
        
		if (ctrlCfg) {
			ret = 3;
			if (rd <= d && d < ru) {
				ret = 1;
			} else if (ru <= d && d < lu) {
				ret = 2;
			} else if (ld <= d && d < 0) {
				ret = 0;
			}
		}else{
			if (0 < touchScript.GetTouch ()) {
				lastTouch = touchScript.GetTouch ();
			}
			Debug.Log (lastTouch);
			if (lastTouch == 4) {ret=0;}//もし、下向きなら!
			if (lastTouch == 1) {ret=1; }//右!
			if (lastTouch == 3) { ret=2; }//上!!
			if (lastTouch == 2) {ret=3; }//左!!
		}
		/*
        if (Input.GetKeyDown(KeyCode.DownArrow)) { ret = 0; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { ret = 2; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { ret = 1; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { ret = 3; }
		*/
    }
    //GUIこうしん(デバックじ以外はコメントアウトで)
    /* void OnGUI()
     {
         if (acc != null)
         {
             float x = Screen.width / 10;
             float y = 0;
             float w = Screen.width * 8 / 10;
             float h = Screen.height / 20;

             for (int i = 0; i < 4; i++)
             {
                 y = Screen.height / 10 + h * i;
                 string text = string.Empty;

                 switch (i)
                 {

                     case 0:
                         text = string.Format("x:{0}", System.Math.Round(this.acc.x, 3));
                         break;
                     case 1:
                         text = string.Format("y:{0}", System.Math.Round(this.acc.y, 3));
                         break;
                     case 2:
                         text = string.Format("z:{0}", System.Math.Round(this.acc.z, 3));
                         break;
                     case 3:
                         text = string.Format("D:{0}", ret);
                         break;
                     default:
                         throw new System.InvalidOperationException();

                 }
                 GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);

             }
         }
     }*/

    public int getDirection()
    {
        return ret;
    }


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections;

public class share : MonoBehaviour
{
    public void OnTweet()
    {
        StartCoroutine(Share());
    }

    IEnumerator Share()
    {
        Debug.Log("Sharing");
        Debug.Log(Application.persistentDataPath);
        // 画面をキャプチャ
		Application.CaptureScreenshot("screenShot.png");

        // キャプチャを保存するので１フレーム待つ
        yield return new WaitForEndOfFrame();

        // シェアテキスト設定
        string text = "うんこくいたい切実";
        string url = "http://twitter.com/";
        yield return new WaitForSeconds(1);

        // キャプチャの保存先を指定
        string texture_url = Application.persistentDataPath + "/screenShot.png";

        // iOS側の処理を呼び出す
        Debug.Log("im living");
        SocialConnector.SocialConnector.Share(text, url, texture_url);
        yield break;
        //SocialConnector.SocialConnector.Share (text);
        Debug.Log("end");
    }
}
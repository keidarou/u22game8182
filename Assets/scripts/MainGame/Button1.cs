using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button1 : MonoBehaviour
{
    //かえた！

    public GameObject gameobj;
    public GameObject pause;
    public GameObject restart;
    public GameObject quit;
    public GameObject Resume;
    GameObject timee;
    public bool automaticmode = true;
    movetheballautomatic script;
    timelimitandmemory timescript;
    public void pausebottun()
    {
        if (automaticmode)
        {
            timee = GameObject.Find("timecounter");
            timescript = timee.GetComponent<timelimitandmemory>();
            if (!timescript.gameoverflag)
            {
                script.pauseflag = true;
            }
        }
        else
        {
            if (!script.clearflag)
            {
                script.pauseflag = true;
            }
        }
    }
    public void restartbutton()
    {
        script.restartflag = true;
        script.pauseflag = false;
    }
    public void restartgameover()//オートマティックモードのときのみ
    {
            timee = GameObject.Find("timecounter");
        script.clearflag = false;
        timescript = timee.GetComponent<timelimitandmemory>();
        timescript.pauseorquitflag = true;
    }
    public void quitbutton()
    {
        if (automaticmode)
        {
            timee = GameObject.Find("timecounter");
            Destroy(timee);
        }
        SceneManager.LoadScene("Title");
    }
    public void Resumesuru()
    {
        script.pauseflag = false;
    }

    // Use this for initialization
    void Start()
    {
        script = gameobj.GetComponent<movetheballautomatic>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script.pauseflag);
        if (script.pauseflag == false)
        {
            restart.SetActive(false);
            quit.SetActive(false);
            pause.SetActive(true);
            Resume.SetActive(false);
        }
        else
        {
            restart.SetActive(true);
            quit.SetActive(true);
            pause.SetActive(false);
            Resume.SetActive(true);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputfieldanimation : MonoBehaviour {

    [SerializeField]
    public Animator input;
    public Animator toggle;
    public GameObject toggleObj;
    //public GameObject leftbutton, rightbutton,
    public GameObject touch;
    public GameObject textfield;
    touch script;

    void Start()
    {
        script = touch.GetComponent<touch>();
        input = textfield.GetComponent<Animator>();
        textfield.SetActive(false);
        toggleObj.SetActive(false);
    }

    public int muki = 0;
    int touchinfo;

    public void animationend()
    {
        textfield.SetActive(false);
        toggleObj.SetActive(false);
    }
    public void rightt() { rightchange(); }
    public void leftt() { leftchange(); }

    public void leftchange()
    {
        if (muki == 0)
        {
            //leftbutton.SetActive(false);
            muki = -1;
        }
        else if (muki == 1)
        {
            //rightbutton.SetActive(true);
            input.SetTrigger("Inputfieldout");
            toggle.SetTrigger("toggleoutt");
            muki = 0;
        }

    }
    public void rightchange()
    {
        if (muki == 0)
        {
            muki = 1;
            //rightbutton.SetActive(false);
            textfield.SetActive(true);
            input.SetTrigger("Inputfieldstart");

            toggleObj.SetActive(true);
            toggle.SetTrigger("toggleinn");
        }
        if (muki == -1)
        {
            muki = 0;
        }

    }
    public void textField()
    {

    }

}

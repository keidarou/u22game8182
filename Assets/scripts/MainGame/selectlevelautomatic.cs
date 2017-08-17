using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class selectlevelautomatic : MonoBehaviour {

	public void normal()
    {
        SceneManager.LoadScene("endlessmodeeasy");
    }
    public void easy()
    {
        SceneManager.LoadScene("endlessmoderealeasy");
    }
    public void hard()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class noclick : MonoBehaviour , clickbutton.Inoclick
{
    public TestRecorder ob;
    public void OnEyeControllerHit(bool isOn)
    {
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.white;
    }

    public void OnEyeControllerClick()
    {
        ob.NotRestart();
        // 視線マーカーでクリックしたら SceneManager.LoadScene("scene1");シーンを変える
       
    }
}

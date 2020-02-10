using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
public class yesclick_f : MonoBehaviour, clickbutton.Iyesclick_f
{
    public allrecord all;
    public float time = 16f;
    public void OnEyeControllerHit(bool isOn)
    {
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.white;
    }

    public void OnEyeControllerClick()
    {
        Debug.Log("yes");
        all.yesfclick();       
      //  all.onrecord();
 

    }
}

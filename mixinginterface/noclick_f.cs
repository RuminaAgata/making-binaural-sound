using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class noclick_f : MonoBehaviour, clickbutton.Inoclick_f
{
    [SerializeField] public GameObject Instrument1;
    [SerializeField] public GameObject Instrument2;
    [SerializeField] public GameObject Instrument3;
    [SerializeField] public GameObject Instrument4;
    [SerializeField] public GameObject Instrument5;

    public static bool pianorecorded;
    public static bool guitarrecorded;
    public static bool bassrecorded;
    public static bool drumsrecorded;
    public static bool micrecorded;

    //Image indicator;
    //RaycastHit hitInfo;
    GameObject hitObject;

    public Canvas canvas;
    public Canvas canvas2;
    public GameObject select;
    public static bool noclicked;
   void Start()
    {
        
        canvas.enabled = false;
        canvas2.enabled = false;
    }
    public static bool getnoclicked()
    {
        return noclicked;
    }
    public void OnEyeControllerHit(bool isOn)
    {
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.white;
    }

    public void OnEyeControllerClick()
    {
        select.SetActive(false);
        canvas.enabled = false;
        canvas2.enabled = true;
        
    }
}

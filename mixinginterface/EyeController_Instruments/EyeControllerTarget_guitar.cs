using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeControllerTarget_guitar : MonoBehaviour, EyeController.IEyeControllerTarget_guitar, clickbutton.IEyeControllerTarget_guitar
{
    public static bool guitarrecorded;
    public void OnEyeControllerHit(bool isOn)
    {
        Debug.Log("hit");
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.white;
    }

    public void OnEyeControllerClick()
    {
        Debug.Log("scene");
        // 視線マーカーでクリックしたらシーンを変える
        SceneManager.LoadScene("guitarrec");
    }
    public void OnEyeControllerClick_f()
    {
        guitarrecorded = TestRecorder.getguitarrec();
        guitarrecorded = false;
        SceneManager.LoadScene("guitarrec");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeControllerTarget_bass : MonoBehaviour, EyeController.IEyeControllerTarget_bass,clickbutton.IEyeControllerTarget_bass
{
    public static bool bassrecorded;
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
        SceneManager.LoadScene("bassrec");
    }
    public void OnEyeControllerClick_f()
    {
        bassrecorded = TestRecorder.getpianorec();
        bassrecorded=false;
        SceneManager.LoadScene("bassrec");
    }
}
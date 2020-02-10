using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeControllerTarget_microphone : MonoBehaviour, EyeController.IEyeControllerTarget_microphone,clickbutton.IEyeControllerTarget_microphone
{
    public static bool micrecorded;
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
        SceneManager.LoadScene("micrec");
    }
    public void OnEyeControllerClick_f()
    {
        micrecorded = TestRecorder.getmicrec();
        micrecorded = false;
        SceneManager.LoadScene("micrec");
    }
}
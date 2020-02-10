using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeControllerTarget_piano : MonoBehaviour, EyeController.IEyeControllerTarget_piano, clickbutton.IEyeControllerTarget_piano
{
    public static bool pianorecorded;
    public void OnEyeControllerHit(bool isOn)
    {
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.white;
    }

    public void OnEyeControllerClick()
    {
        Debug.Log("scene");
        // 視線マーカーでクリックしたらシーンを変える
        SceneManager.LoadScene("pianorec");
    }
    public void OnEyeControllerClick_f()
    {
        Debug.Log("click_f");
        pianorecorded = TestRecorder.getpianorec();
        pianorecorded = false;
        SceneManager.LoadScene("pianorec");
    }
}

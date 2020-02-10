using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeControllerTarget_drums : MonoBehaviour, EyeController.IEyeControllerTarget_drums,clickbutton.IEyeControllerTarget_drums
{
    public static bool drumsrecorded;
    public void OnEyeControllerHit(bool isOn)
    {
        Debug.Log("hit");
        // 視線マーカーがヒットしたら色を変える
        gameObject.GetComponent<Renderer>().material.color = isOn ? new Color(1, 1, 0) : Color.yellow;
    }

    public void OnEyeControllerClick()
    {
        Debug.Log("scene");
        // 視線マーカーでクリックしたらシーンを変える
        SceneManager.LoadScene("drumsrec");
    }
    public void OnEyeControllerClick_f()
    {
        drumsrecorded = TestRecorder.getdrumsrec();
        drumsrecorded = false;
        SceneManager.LoadScene("drumsrec");
    }
}
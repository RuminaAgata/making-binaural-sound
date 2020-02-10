using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basshriru : MonoBehaviour,allrecord.Ibassrestart
{
    private Vector3 velocity;
    private HRIRu hrir_control;
    // Start is called before the first frame update
    private void Start()
    {
        hrir_control = this.GetComponent<HRIRu>();
        hrir_control.Available();
        hrir_control.Play("ShyBass.wav");
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void restart()
    {
        hrir_control.Play("ShyBass.wav");
    }
}

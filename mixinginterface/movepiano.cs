using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class movepiano : MonoBehaviour
{
	// Start is called before the first frame update
	private Animator animator;
	private Vector3 velocity;
    private HRIRu hrir_control;    
    public bool on_laser;
    public bool grab;
    public SteamVR_LaserPointer steamVrLaserPointer;
    //public string my_name="piano";

    [SerializeField]
    Canvas canvas1;
    [SerializeField]
    Canvas canvas2;
    [SerializeField]
    Canvas eyecanvas;
    [SerializeField]
    GameObject select;
    [SerializeField]
    private TestRecorder ob;

    public float time=16f;


    private void Awake()
    {
        //steamVrLaserPointer = GameObject.Find("Controller (right)").GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
        on_laser = false;
        grab = false;
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        hrir_control = this.GetComponent<HRIRu>();
        hrir_control.Available();
        canvas2.enabled = false;
        eyecanvas.enabled = false;
        select.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grab)
        {
            
            float disntance = Mathf.Abs(Vector3.Distance(gameObject.transform.position, steamVrLaserPointer.gameObject.transform.position));            
            Vector3 to_move = steamVrLaserPointer.gameObject.transform.position + steamVrLaserPointer.gameObject.transform.forward * disntance;
            if (to_move.y <= 0)
            {
                
                to_move.y = 0f;
                
            }
            gameObject.transform.position = to_move;
        }
	}

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if (grab) {

            return;
        }
        if (on_laser)
        {
            canvas1.enabled = false;
            grab = true;
            if (SceneManager.GetActiveScene().name == "pianorec") { 
            hrir_control.Play("ShyPiano.wav");
        }
            else if (SceneManager.GetActiveScene().name == "guitarrec")
            {
                hrir_control.Play("ShyGuitar.wav");
            }
            else if (SceneManager.GetActiveScene().name == "bassrec")
            {
                hrir_control.Play("ShyBass.wav");
            }
            else if (SceneManager.GetActiveScene().name == "drumsrec")
            {
                hrir_control.Play("ShyDrums.wav");
            }
            else if (SceneManager.GetActiveScene().name == "micrec")
            {
                hrir_control.Play("ShyVoice.wav");
            }
            ob.StartRecord();
            Invoke("finishrec", time);
        }       
    }
    private void finishrec()
    {
        grab = false;
        canvas2.enabled = true;
        eyecanvas.enabled = true;
        select.SetActive(true);
        
        ob.StopRecord();
        ob.StartGhost();
   //     hrir_control.Stop();
    }
    private void OnPointerOut(object sender, PointerEventArgs e)
    {

        if (e.target.name == this.name) on_laser = false;       
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {        
        if (e.target.name == this.name) on_laser = true;        
    }
    public delegate void functionType();
}
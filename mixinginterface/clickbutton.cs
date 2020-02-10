using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class clickbutton : MonoBehaviour
{
    [SerializeField] public GameObject Instrument1;
    [SerializeField] public GameObject Instrument2;
    [SerializeField] public GameObject Instrument3;
    [SerializeField] public GameObject Instrument4;
    [SerializeField] public GameObject Instrument5;

    Image indicator;
    RaycastHit hitInfo;
    GameObject hitObject;
    public GameObject yes;
    public GameObject no;
    bool hasClicked = false;
    public static bool noclicked;
    public static bool isPlayback;
    void Start()
    {
        indicator = transform.Find("indicator").GetComponent<Image>();
        noclicked = noclick_f.getnoclicked();
        noclicked = false;
    }
    void AnimateIndicator(bool isOn)
    {
        if (isOn)
        {
            indicator.fillAmount += 0.3f * Time.deltaTime;
        }
        else
        {
            indicator.fillAmount = 0;
        }
    }

    void FixedUpdate()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, 58);
        isPlayback = allrecord.getisPlayBack();
     //   noclicked = noclick_f.getnoclicked();
        if (hasHit)
        {
            //ターゲットが変更された場合
            if (hitObject != hitInfo.collider.gameObject)
            {
                // 以前のターゲットを無効に
                if (hitObject)
                {
                    AnimateIndicator(false);
                    DispatchHitEvent(false);
                }
                //ヒットイベント発行
                hasClicked = false;
                hitObject = hitInfo.collider.gameObject;
                DispatchHitEvent(true);

            }

            else
            {

                //インジケーターアニメーション開始
                if (hasClicked == false)
                {
                    if (noclicked == false)
                    {
                        if (hitObject == yes || hitObject == no)
                        {
                            AnimateIndicator(true);
                        }

                    }
                    else if (noclicked == true)
                    {
                        if (hitObject != yes || hitObject != no)
                        {
                            AnimateIndicator(true);
                        }

                    }
                }

                //インジケーターが100％になったらクリックイベント発行
                if (indicator.fillAmount >= 1)
                {
                    Debug.Log("perfect");
                    hasClicked = true;
                    indicator.fillAmount = 0;
                    DispatchClickEvent();
                }
            }

        }
        else
        {

            //インジケーターアニメーション停止
            AnimateIndicator(false);
            DispatchHitEvent(false);
            hitObject = null;
            hasClicked = false;
        }
    }

    public interface Inoclick
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface Iyesclick
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface Inoclick_f
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface Iyesclick_f
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface IEyeControllerTarget_piano
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
        void OnEyeControllerClick_f();
    }
    public interface IEyeControllerTarget_guitar
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
        void OnEyeControllerClick_f();
    }
    public interface IEyeControllerTarget_bass
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
        void OnEyeControllerClick_f();
    }
    public interface IEyeControllerTarget_drums
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
        void OnEyeControllerClick_f();
    }
    public interface IEyeControllerTarget_microphone
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
        void OnEyeControllerClick_f();
    }
    void DispatchHitEvent(bool isOn)
    {
        if (noclicked == false)
        {
            if (hitObject == yes)
            {
                if (SceneManager.GetActiveScene().name != "finishscene")
                {
                    var target = hitObject.GetComponent<Iyesclick>();
                    if (target != null)
                    {
                        target.OnEyeControllerHit(isOn);
                    }
                }
                else if (SceneManager.GetActiveScene().name == "finishscene")
                {
                    var target = hitObject.GetComponent<Iyesclick_f>();
                    if (target != null)
                    {
                        target.OnEyeControllerHit(isOn);
                    }
                }
            }
            else if (hitObject == no)
            {
                if (SceneManager.GetActiveScene().name != "finishscene")
                {
                    var target = hitObject.GetComponent<Inoclick>();
                    if (target != null)
                    {
                        target.OnEyeControllerHit(isOn);
                    }
                }
                else if (SceneManager.GetActiveScene().name == "finishscene")
                {
                    var target = hitObject.GetComponent<Inoclick_f>();
                    if (target != null)
                    {
                        hasClicked = false;
                        target.OnEyeControllerHit(isOn);
                    }
                }
            }
        }

        else if (noclicked == true)
        {
            if (hitObject == Instrument1)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_piano>();
                if (target != null)
                {
                    Debug.Log(target);
                    target.OnEyeControllerHit(isOn);
                }
            }
            else if (hitObject == Instrument2)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_guitar>();
                if (target != null)
                {
                    Debug.Log(target);
                    target.OnEyeControllerHit(isOn);
                }
            }
            else if (hitObject == Instrument3)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_bass>();
                if (target != null)
                {
                    target.OnEyeControllerHit(isOn);
                }
            }
            else if (hitObject == Instrument4)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_drums>();
                if (target != null)
                {
                    target.OnEyeControllerHit(isOn);
                }
            }
            else if (hitObject == Instrument5)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_microphone>();
                if (target != null)
                {
                    target.OnEyeControllerHit(isOn);
                }
            }
        }
    }
    
    void DispatchClickEvent()
    {

        if (hitObject)
        {
            if (noclicked == false)
            {
                if (hitObject == yes)
                {
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        var target = hitObject.GetComponent<Iyesclick>();
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        var target = hitObject.GetComponent<Iyesclick_f>();
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                }
                else if (hitObject == no)
                {
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        var target = hitObject.GetComponent<Inoclick>();
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        var target = hitObject.GetComponent<Inoclick_f>();
                        if (target != null)
                        {
                            noclicked = true;
                            Debug.Log(noclicked);
                            target.OnEyeControllerClick();
                        }
                    }
                }

            }
            else if (noclicked == true)
            {
                if (hitObject == Instrument1)
                {
                    var target = hitObject.GetComponent<IEyeControllerTarget_piano>();
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        if (target != null)
                        {
                      //      noclicked = noclick_f.getnoclicked();
                            noclicked = false;
                            target.OnEyeControllerClick_f();
                        }
                    }
                }
                else if (hitObject == Instrument2)
                {

                    var target = hitObject.GetComponent<IEyeControllerTarget_guitar>();
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {

                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        if (target != null)
                        {
                          //  noclicked = noclick_f.getnoclicked();
                            noclicked = false;
                            target.OnEyeControllerClick_f();
                        }
                    }
                }
                else if (hitObject == Instrument3)
                {
                    var target = hitObject.GetComponent<IEyeControllerTarget_bass>();
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        if (target != null)
                        {
                            noclicked = false;
                            target.OnEyeControllerClick_f();
                        }
                    }
                }
                else if (hitObject == Instrument4)
                {
                    var target = hitObject.GetComponent<IEyeControllerTarget_drums>();
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        if (target != null)
                        {
                            noclicked = false;
                            target.OnEyeControllerClick_f();
                        }
                    }
                }
                else if (hitObject == Instrument5)
                {
                    var target = hitObject.GetComponent<IEyeControllerTarget_microphone>();
                    if (SceneManager.GetActiveScene().name != "finishscene")
                    {
                        if (target != null)
                        {
                            target.OnEyeControllerClick();
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "finishscene")
                    {
                        if (target != null)
                        {
                            noclicked = false;
                            target.OnEyeControllerClick_f();
                        }
                    }
                }
            }
        }
    }
}
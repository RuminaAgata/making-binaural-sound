using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using System.Collections;
//using System.Collections.Generic;

public class EyeController : MonoBehaviour
{
    [SerializeField] public GameObject Instrument1;
    [SerializeField] public GameObject Instrument2;
    [SerializeField] public GameObject Instrument3;
    [SerializeField] public GameObject Instrument4;
    [SerializeField] public GameObject Instrument5;
    public static bool pianorec = false;
    public static bool guitarrec = false;
    public static bool bassrec = false;
    public static bool drumsrec = false;
    public static bool micrec = false;

    // Start is called before the first frame update
    Image indicator;
    RaycastHit hitInfo;
    GameObject hitObject;
    bool hasClicked = false;

    void Start()
    { 
     indicator = transform.Find("indicator").GetComponent<Image>();
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
    // Update is called once per frame
    void FixedUpdate()
    {

        // 物理オブジェクトのヒットテスト
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, 58);
        if (hasHit)
        {
            
            //ターゲットが変更された場合
            if (hitObject != hitInfo.collider.gameObject)
            {
                Debug.Log(hitObject);
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
                    AnimateIndicator(true);
                }

                //インジケーターが100％になったらクリックイベント発行
                if (indicator.fillAmount >= 1)
                {
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

    
    public interface IEyeControllerTarget_piano
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface IEyeControllerTarget_guitar
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface IEyeControllerTarget_bass
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface IEyeControllerTarget_drums
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    public interface IEyeControllerTarget_microphone
    {
        void OnEyeControllerHit(bool isOn);
        void OnEyeControllerClick();
    }
    void DispatchHitEvent(bool isOn)
    {

        if (hitObject)
        {
            if (hitObject == Instrument1)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_piano>();
                if (target != null)
                {
                    target.OnEyeControllerHit(isOn);
                }
            }
            else if (hitObject == Instrument2)
            {
                var target = hitObject.GetComponent<IEyeControllerTarget_guitar>();
                if (target != null)
                {
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
        if (hitObject == Instrument1)
        {
            var target = hitObject.GetComponent<IEyeControllerTarget_piano>();
            if (target != null)
            {
                target.OnEyeControllerClick();
            }
        }
        else if (hitObject == Instrument2)
        {
             var target = hitObject.GetComponent<IEyeControllerTarget_guitar>();
            if (target != null)
            {
                target.OnEyeControllerClick();
            }
        }
        else if (hitObject == Instrument3)
        {
            var target = hitObject.GetComponent<IEyeControllerTarget_bass>();
            if (target != null)
            {
                target.OnEyeControllerClick();
            }
        }
        else if (hitObject == Instrument4)
        {
            var target = hitObject.GetComponent<IEyeControllerTarget_drums>();
            if (target != null)
            {
                target.OnEyeControllerClick();
            }
        }
        else if (hitObject == Instrument5)
        {
            var target = hitObject.GetComponent<IEyeControllerTarget_microphone>();
            if (target != null)
            {
                target.OnEyeControllerClick();
            }
        }
    }
}

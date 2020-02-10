using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class allrecord : MonoBehaviour
{
    //　保存先フォルダ
    private string saveDataFolder = "/Ghost";
    //　保存ファイル名
    private string saveFileName_p = "/pianodata.dat";
    private string saveFileName_g = "/guitardata.dat";
    private string saveFileName_b = "/bassdata.dat";
    private string saveFileName_d = "/drumsdata.dat";
    private string saveFileName_m = "/micdata.dat";
    //保存データ
    private GhostData pianoData;
    private GhostData guitarData;
    private GhostData bassData;
    private GhostData drumsData;
    private GhostData micData;
    //　ゴースト用キャラ
    [SerializeField]
    private GameObject ghost_p;
    [SerializeField]
    private GameObject ghost_g;
    [SerializeField]
    private GameObject ghost_b;
    [SerializeField]
    private GameObject ghost_d;
    [SerializeField]
    private GameObject ghost_m;

    [SerializeField]
    private float recordDuration = 0.005f; //0.005f
    [SerializeField]
    public Canvas canvas;
    public GameObject select;
    public float time = 16f;
    //　再生中かどうか
    public static bool isPlayBack;
    public GameObject cameraa;
    public Text texta;
    int waittime = 3;
    //　ゴーストデータが1周りした後の待ち時間
    // [SerializeField]
    // private float waitTime = 2f;

    public static bool getisPlayBack()
    {
        return isPlayBack;
    }

    private class GhostData
    {
        //　位置のリスト
        public List<Vector3> posLists = new List<Vector3>();
        //　角度リスト
        public List<Quaternion> rotLists = new List<Quaternion>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraa.GetComponent<recordfunc>().enabled = false;
        Load();
        StartGhost();
        canvas.enabled = false;
        select.SetActive(false);
        Invoke("finishghost", time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onrecord()
    {
        cameraa.GetComponent<recordfunc>().enabled = true;
    }
    public void Load()
    {

        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName_p))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName_p);
            //　ghostDataに読み込んだデータを書き込む
            if (pianoData == null)
            {
                pianoData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, pianoData);

        }
        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName_g))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName_g);
            //　ghostDataに読み込んだデータを書き込む
            if (guitarData == null)
            {
                guitarData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, guitarData);

        }
        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName_b))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName_b);
            //　ghostDataに読み込んだデータを書き込む
            if (bassData == null)
            {
                bassData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, bassData);

        }
        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName_d))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName_d);
            //　ghostDataに読み込んだデータを書き込む
            if (drumsData == null)
            {
                drumsData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, drumsData);

        }
        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName_m))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName_m);
            //　ghostDataに読み込んだデータを書き込む
            if (micData == null)
            {
                micData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, micData);

        }
        Debug.Log("ゴーストデータをロードしました。");
    }
    public void StartGhost()
    {
        Debug.Log("StartGhost");
        if (pianoData == null || guitarData == null || bassData == null || drumsData == null || micData == null)
        {
            Debug.Log("ゴーストデータがありません");
        }
        else
        {
            isPlayBack = true;
            ghost_p.transform.position = pianoData.posLists[0];
            ghost_p.transform.rotation = pianoData.rotLists[0];
            ghost_g.transform.position = guitarData.posLists[0];
            ghost_g.transform.rotation = guitarData.rotLists[0];
            ghost_b.transform.position = bassData.posLists[0];
            ghost_b.transform.rotation = bassData.rotLists[0];
            ghost_d.transform.position = drumsData.posLists[0];
            ghost_d.transform.rotation = drumsData.rotLists[0];
            ghost_m.transform.position = micData.posLists[0];
            ghost_m.transform.rotation = micData.rotLists[0];
            StartCoroutine(PlayBack());
        }
    }
    IEnumerator PlayBack()
    {

        var i = 0;

        Debug.Log("データ数: " + pianoData.posLists.Count);


        while (isPlayBack)
        {
            yield return new WaitForSeconds(recordDuration);

            ghost_p.transform.position = pianoData.posLists[i];
            ghost_p.transform.rotation = pianoData.rotLists[i];
            ghost_g.transform.position = guitarData.posLists[i];
            ghost_g.transform.rotation = guitarData.rotLists[i];
            ghost_b.transform.position = bassData.posLists[i];
            ghost_b.transform.rotation = bassData.rotLists[i];
            ghost_d.transform.position = drumsData.posLists[i];
            ghost_d.transform.rotation = drumsData.rotLists[i];
            ghost_m.transform.position = micData.posLists[i];
            ghost_m.transform.rotation = micData.rotLists[i];
            i++;

            //　保存データ数を超えたら最初から再生
            if (i >= pianoData.posLists.Count)// || i >= guitarData.posLists.Count || i >= bassData.posLists.Count || i >= drumsData.posLists.Count || i >= micData.posLists.Count)
            {

                //　アニメーション途中で終わった時用に待ち時間を入れる
                //  yield return new WaitForSeconds(waitTime);

                ghost_p.transform.position = pianoData.posLists[0];
                ghost_p.transform.rotation = pianoData.rotLists[0];
                ghost_g.transform.position = guitarData.posLists[0];
                ghost_g.transform.rotation = guitarData.rotLists[0];
                i = 0;
            }
        }
    }
    public void finishghost()
    {
        isPlayBack = false;
        canvas.enabled = true;
        select.SetActive(true);
        Debug.Log("finish ghost");
        ghost_p.transform.position = pianoData.posLists[0];
        ghost_p.transform.rotation = pianoData.rotLists[0];
        ghost_g.transform.position = guitarData.posLists[0];
        ghost_g.transform.rotation = guitarData.rotLists[0];
        ghost_b.transform.position = bassData.posLists[0];
        ghost_b.transform.rotation = bassData.rotLists[0];
        ghost_d.transform.position = drumsData.posLists[0];
        ghost_d.transform.rotation = drumsData.rotLists[0];
        ghost_m.transform.position = micData.posLists[0];
        ghost_m.transform.rotation = micData.rotLists[0];

    }
    public void yesfclick()
    {
        canvas.enabled = false;
        select.SetActive(false);
        texta.text = waittime.ToString();
        StartCoroutine("wait");
        ghost_p.transform.position = pianoData.posLists[0];
        ghost_p.transform.rotation = pianoData.rotLists[0];
        ghost_g.transform.position = guitarData.posLists[0];
        ghost_g.transform.rotation = guitarData.rotLists[0];
        ghost_b.transform.position = bassData.posLists[0];
        ghost_b.transform.rotation = bassData.rotLists[0];
        ghost_d.transform.position = drumsData.posLists[0];
        ghost_d.transform.rotation = drumsData.rotLists[0];
        ghost_m.transform.position = micData.posLists[0];
        ghost_m.transform.rotation = micData.rotLists[0];
    }
    public void finishrecording()
    {
        texta.text = "Finish!";

    }
    IEnumerator wait()
    {
        var target1 = ghost_p.GetComponent<Ipianorestart>();
        var target2 = ghost_g.GetComponent<Iguitarrestart>();
        var target3 = ghost_b.GetComponent<Ibassrestart>();
        var target4 = ghost_d.GetComponent<Idrumsrestart>();
        var target5 = ghost_m.GetComponent<Imicrestart>();

        yield return new WaitForSeconds(1);
        waittime -= 1;
        texta.text = waittime.ToString();
        yield return new WaitForSeconds(1);
        waittime -= 1;
        texta.text = waittime.ToString();
        yield return new WaitForSeconds(1);
        waittime -= 1;
        texta.text = "Recording";
        StartGhost();
        target1.restart();
        target2.restart();
        target3.restart();
        target4.restart();
        target5.restart();
        Invoke("finishrecording", time);
        onrecord();
       
    }
    public interface Ipianorestart
    {
        void restart();
    }
    public interface Iguitarrestart
    {
        void restart();
    }
    public interface Ibassrestart
    {
        void restart();
    }
    public interface Idrumsrestart
    {
        void restart();
    }
    public interface Imicrestart
    {
        void restart();
    }
}

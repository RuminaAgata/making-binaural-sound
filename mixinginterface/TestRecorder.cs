using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class TestRecorder : MonoBehaviour
{
    //　操作楽器
    [SerializeField]
    private movepiano ghostChara;
    // AnimatorController
    private Animator animator;
    //　現在記憶しているかどうか
    private bool isRecord;
    //　記録間隔
    [SerializeField]
    private float recordDuration = 0.005f; //0.005f
                                           //　経過時間
    private float elapsedTime = 0f;
    //　ゴーストデータ
    private GhostData ghostData;
    //　再生中かどうか
    private bool isPlayBack;
    //　ゴースト用キャラ
   // [SerializeField]
    private GameObject ghost=null;
    //　ゴーストデータが1周りした後の待ち時間
    [SerializeField]
    private float waitTime = 0f;
    //　保存先フォルダ
    private string saveDataFolder = "/Ghost";
    //　保存ファイル名
    private string saveFileName; //= "/pianodata.dat";
    //録音してるかどうか
    public static bool pianorec = false;
    public static bool guitarrec = false;
    public static bool bassrec = false;
    public static bool drumsrec = false;
    public static bool micrec = false;
    //　ゴーストデータクラス

    public GameObject  ghost_prefab;
    int i = 0;

    [Serializable]
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
        // animator = ghostChara.GetComponent<Animator>();
        //ghost.SetActive(false);
        StopGhost();
        if (SceneManager.GetActiveScene().name=="pianorec") {
            saveFileName = "/pianodata.dat";
        }
        else if (SceneManager.GetActiveScene().name == "guitarrec")
        {
            saveFileName = "/guitardata.dat";
        }
        else if (SceneManager.GetActiveScene().name == "bassrec")
        {
            saveFileName = "/bassdata.dat";
        }
        else if (SceneManager.GetActiveScene().name == "drumsrec")
        {
            saveFileName = "/drumsdata.dat";
        }
        else if (SceneManager.GetActiveScene().name == "micrec")
        {
            saveFileName = "/micdata.dat";
        }
    }
    
    // Update is called once per frame
    void Update()//edit>projectsetting>time
    {
      
        if (isRecord)
        {

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= recordDuration)
            {
                ghostData.posLists.Add(ghostChara.transform.position);
                ghostData.rotLists.Add(ghostChara.transform.rotation);
                elapsedTime = 0f;
            }
        }
        if (isPlayBack)
        {
            
            ghost.transform.position = ghostData.posLists[i];
            ghost.transform.rotation = ghostData.rotLists[i];
            i++;
            if (i >= ghostData.posLists.Count)
            {

                //　アニメーション途中で終わった時用に待ち時間を入れる
              //  yield return new WaitForSeconds(waitTime);

                ghost.transform.position = ghostData.posLists[0];
                ghost.transform.rotation = ghostData.rotLists[0];

                i = 0;
            }
        }
    }
    //　キャラクターデータの保存
    public void StartRecord()
    {
        //　保存する時はゴーストの再生を停止
     
        
        /*if (onlyone = true)//変更
		{
			Debug.Log("Restart from the beginning");
			return;
		}*/
        isRecord = true;
        elapsedTime = 0f;
        ghostData = new GhostData();
        //onlyone = true;

        Debug.Log("StartRecord");
    }

    //　キャラクターデータの保存の停止
    public void StopRecord()
    {
        isRecord = false;
        Debug.Log("StopRecord");
        smoothing();

    }
    public void smoothing()
    {
        /*for (int j = 2; j < ghostData.posLists.Count-2; ++j)
        {
            ghostData.posLists[j] = (ghostData.posLists[j-2]+ 2f*ghostData.posLists[j - 1] + 3f*ghostData.posLists[j]+ 2f*ghostData.posLists[j + 1]+ ghostData.posLists[j+2])/9f;
        }*/
        for (int j = 2; j < ghostData.posLists.Count - 2; ++j)
        {
            ghostData.posLists[j] = (ghostData.posLists[j - 2] + ghostData.posLists[j - 1] +ghostData.posLists[j] +ghostData.posLists[j + 1] + ghostData.posLists[j + 2]) / 5f;
        }
    }
    //　ゴーストの再生ボタンを押した時の処理
    public void StartGhost()
    {
        Debug.Log("StartGhost");
       
            ghostChara.transform.position = ghostData.posLists[0];
            ghostChara.transform.rotation = ghostData.rotLists[0];
            ghost = Instantiate(ghost_prefab);
        if (ghostData == null)
        {
            Debug.Log("ゴーストデータがありません");
        }
        else
        {
            isRecord = false;
            ghost.transform.position = ghostData.posLists[0];
            ghost.transform.rotation = ghostData.rotLists[0];
            isPlayBack = true;
            ghost.SetActive(true);
            //StartCoroutine(PlayBack());
        }
    }

    //　ゴーストの停止
    public void StopGhost()
    {
        Debug.Log("StopGhost");        
       // StopAllCoroutines();
        isPlayBack = false;
        if (ghost != null) { Destroy(ghost); ghost = null; }        
    }

    //　ゴーストの再生
   /* IEnumerator PlayBack()
    {

        var i = 0;

        Debug.Log("データ数: " + ghostData.posLists.Count);

        while (isPlayBack)
        {

            yield return new WaitForSeconds(recordDuration);

            ghost.transform.position = ghostData.posLists[i];
            ghost.transform.rotation = ghostData.rotLists[i];


            i++;

            //　保存データ数を超えたら最初から再生
            if (i >= ghostData.posLists.Count)
            {

                //　アニメーション途中で終わった時用に待ち時間を入れる
                yield return new WaitForSeconds(waitTime);

                ghost.transform.position = ghostData.posLists[0];
                ghost.transform.rotation = ghostData.rotLists[0];

                i = 0;
            }
        }
    }*/

    public void Save()
    {
        if (ghostData != null)
        {
            //　GhostDataクラスをJSONデータに書き換え
            var data = JsonUtility.ToJson(ghostData);
            //　ゲームフォルダにファイルを作成
            File.WriteAllText(Application.dataPath + saveDataFolder + saveFileName, data);
            Debug.Log(Application.dataPath + saveDataFolder + saveFileName);
            Debug.Log("ゴーストデータをセーブしました");
        }
    }


    public void Load()
    {

        if (File.Exists(Application.dataPath + saveDataFolder + saveFileName))
        {
            string readAllText = File.ReadAllText(Application.dataPath + saveDataFolder + saveFileName);
            //　ghostDataに読み込んだデータを書き込む
            if (ghostData == null)
            {
                ghostData = new GhostData();
            }
            JsonUtility.FromJsonOverwrite(readAllText, ghostData);
            Debug.Log("ゴーストデータをロードしました。");
        }
    }
    public void Restart()
    {
        
        File.Delete(Application.dataPath + saveDataFolder + saveFileName);
        Debug.Log("Restart");
        LoadS();
    }
    public void LoadS()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    void OnApplicationQuit()
    {
        Debug.Log("{アプリケーション終了}");

    }
    public void NotRestart()
    {
        Debug.Log("NotRestart");
        Save();
        if (SceneManager.GetActiveScene().name == "pianorec")
        {
            pianorec = true;
        }
        else if (SceneManager.GetActiveScene().name == "guitarrec")
        {
            guitarrec = true;
        }
        else if (SceneManager.GetActiveScene().name == "bassrec")
        {
            bassrec = true;
        }
        else if (SceneManager.GetActiveScene().name == "drumsrec")
        {
            drumsrec = true;
        }
        else if (SceneManager.GetActiveScene().name == "micrec")
        {
            micrec = true;
        }
        SceneManager.LoadScene("scene1");
    }
    public static bool getpianorec()
    {
        return pianorec;

    }
    public static bool getguitarrec()
    {
        return guitarrec;
    }
    public static bool getbassrec()
    {
        return bassrec;
    }
    public static bool getdrumsrec()
    {
        return drumsrec;
    }
    public static bool getmicrec()
    {
        return micrec;
    }
}
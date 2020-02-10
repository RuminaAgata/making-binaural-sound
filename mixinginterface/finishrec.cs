using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishrec : MonoBehaviour
{
    public static bool pianorecorded;
    public static bool guitarrecorded;
    public static bool bassrecorded;
    public static bool drumsrecorded;
    public static bool micrecorded;
    // Start is called before the first frame update
    void Start()
    {
        pianorecorded = TestRecorder.getpianorec();
        guitarrecorded = TestRecorder.getguitarrec();
        bassrecorded = TestRecorder.getbassrec();
        drumsrecorded = TestRecorder.getdrumsrec();
        micrecorded = TestRecorder.getmicrec();
        finishscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void finishscene()
    {
        if (pianorecorded == true && guitarrecorded == true && bassrecorded == true && drumsrecorded == true && micrecorded == true)
        {
            SceneManager.LoadScene("finishscene");
        }
    }
}

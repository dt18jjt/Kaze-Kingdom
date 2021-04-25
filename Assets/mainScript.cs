using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainScript : MonoBehaviour
{
    startScript start;
    
    // Start is called before the first frame update
    void Start()
    {
        //start = GameObject.Find("G").GetComponent<startScript>();

    }
    public void Play()
    {
        //start.levelStart();
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Main");
    }
    public void Exit()
    {
        Application.Quit();
    }

}

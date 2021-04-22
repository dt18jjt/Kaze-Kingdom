using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    startScript start;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("G").GetComponent<startScript>();
    }
    public void Play()
    {
        start.main = false;
        start.levelStart();
    }
    public void Exit()
    {
        Application.Quit();
    }
}

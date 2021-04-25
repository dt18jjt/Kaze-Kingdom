using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    public int tNum = 0;
    startScript start;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("G").GetComponent<startScript>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Skip()
    {
        start.levelStart();
    }
    public void Next()
    {
        tNum++;
    }
    public void Back()
    {
        tNum--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorialScript : MonoBehaviour
{
    public int tNum = 0;
    public GameObject next, back, play, mov, cam, obj, prog, dstr;
    private EventSystem system;
    startScript start;
    
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("G").GetComponent<startScript>();
        //system = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (tNum)
        {
            case 0:
                //next.SetActive(true);
                back.SetActive(false);
                play.SetActive(false);
                mov.SetActive(true);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(false);
                break;
            case 1:
                //next.SetActive(true);
                back.SetActive(true);
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(true);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(false);
                break;
            case 2:
                //next.SetActive(true);
                play.SetActive(false);
                back.SetActive(true);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(true);
                prog.SetActive(false);
                dstr.SetActive(false);
                break;
            case 3:
                //next.SetActive(true);
                back.SetActive(true);
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(true);
                dstr.SetActive(false);
                break;
            case 4:
                //next.SetActive(false);
                back.SetActive(true);
                play.SetActive(true);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(true);
                break;
        }
    }
    public void Skip()
    {
        start.levelStart();
    }
    public void Next()
    {
        if(tNum < 4)
            tNum++;
    }
    public void Back()
    {
        if(tNum < 0)
            tNum--;
    }
}

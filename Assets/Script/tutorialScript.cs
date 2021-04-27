using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorialScript : MonoBehaviour
{
    public int tNum = 0;
    public GameObject next, back, play, mov, cam, obj, prog, dstr, pse;
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
                play.SetActive(false);
                mov.SetActive(true);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(false);
                pse.SetActive(false);
                break;
            case 1:
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(true);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(false);
                pse.SetActive(false);
                break;
            case 2:
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(true);
                prog.SetActive(false);
                dstr.SetActive(false);
                pse.SetActive(false);
                break;
            case 3:
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(true);
                dstr.SetActive(false);
                pse.SetActive(false);
                break;
            case 4:
                play.SetActive(false);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(true);
                pse.SetActive(false);
                break;
            case 5:
                play.SetActive(true);
                mov.SetActive(false);
                cam.SetActive(false);
                obj.SetActive(false);
                prog.SetActive(false);
                dstr.SetActive(false);
                pse.SetActive(true);
                break;
        }
    }
    public void Skip()
    {
        start.levelStart();
    }
    public void Next()
    {
        if(tNum < 5)
            tNum++;
    }
    public void Back()
    {
        if(tNum > 0)
            tNum--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    public float timer;
    public float comboTime;
    public float comboTimeAmt = 5;
    public int comboNum;
    public bool comboOn = true;
    public Text timeText;
    public Text comboText;
    public Image fillImg;
    // Start is called before the first frame update
    void Start()
    {
        comboTime = 0;
        fillImg.fillAmount = comboTime;
       comboText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
        else
        {
            timer = 0;
        }
        if (comboOn && comboTime > 0)
        {
            comboText.enabled = true;          
            comboText.text = comboNum.ToString();
            comboTime -= Time.deltaTime;
            fillImg.fillAmount = comboTime / comboTimeAmt;
            if(comboTime <= 0)
            {
                comboOn = false;
                comboText.enabled = false;
                comboNum = 0;

            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

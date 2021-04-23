using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public float timer, comboTime, comboTimeAmt = 5;
    public int comboNum;
    public bool comboOn = true, paused;
    public Text timeText, comboText;
    public Image fillImg;
    startScript start;
    // Start is called before the first frame update
    void Start()
    {
       comboTime = 0;
       fillImg.fillAmount = comboTime;
       comboText.enabled = false;
       start = GetComponent<startScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (start.playing && !paused)
        {
            timer -= (timer > 0) ? Time.deltaTime : 0;
            DisplayTime(timer);      
        }
        if (comboOn && comboTime > 0 && !paused)
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
        Time.timeScale = (paused) ? 0.1f : 1f;
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button7))
        {
            if (!paused && start.playing)
            {
                paused = true;
                SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            }
            else
                paused = false;
                
        }

    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //timeText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = (minutes >= 1) ? minutes.ToString() : seconds.ToString();
        timeText.color = (minutes >= 1) ? Color.white : Color.red;
    }

}

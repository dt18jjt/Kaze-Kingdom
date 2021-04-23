using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public float timer, comboTime, comboTimeAmt = 5, seconds, minutes;
    public int comboNum;
    public bool comboOn = true, paused;
    public Text timeText, comboText;
    public Image cmbfillImg, timfillImg;
    startScript start;
    // Start is called before the first frame update
    void Start()
    {
       comboTime = 0;
       cmbfillImg.fillAmount = comboTime;
       timfillImg.fillAmount = 0;
       comboText.enabled = false;
       start = GetComponent<startScript>();

    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //timeText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = (minutes >= 1) ? minutes.ToString() : seconds.ToString();
        timeText.color = (minutes >= 1) ? Color.white : Color.red;
    }
    // Update is called once per frame
    void Update()
    {
        //Timer countdown
        if (start.playing && !paused)
        {
            timer -= (timer > 0) ? Time.deltaTime : 0;
            DisplayTime(timer);
            timfillImg.fillAmount = (minutes >= 1) ? seconds / 60: 0;
        }
        //Combo meter
        if (comboOn && comboTime > 0 && !paused)
        {
            comboText.enabled = true;          
            comboText.text = comboNum.ToString();
            comboTime -= Time.deltaTime;
            cmbfillImg.fillAmount = comboTime / comboTimeAmt;
            if(comboTime <= 0)
            {
                comboOn = false;
                comboText.enabled = false;
                comboNum = 0;

            }
        }
        Time.timeScale = (paused) ? 0f : 1f;
        //Pausing
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button7))
        {
            if (!paused && start.playing)
            {
                paused = true;
                SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            }
            else
            {
                paused = false;
                SceneManager.UnloadSceneAsync("Pause");
            }
                
                
        }

    }
   

}

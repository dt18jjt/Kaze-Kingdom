using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public float timer, comboTime, comboAdd, comboTimeAmt = 5, seconds, minutes;
    public int comboNum;
    public bool comboOn = true, paused, diasterOn = false;
    public Text timeText, comboText, comboAddText;
    public Image cmbfillImg, timfillImg;
    public GameObject dstr, dstr2;
    startScript start;
    Tornado player;
    // Start is called before the first frame update
    void Start()
    {
       comboTime = 0;
       cmbfillImg.fillAmount = comboTime;
       timfillImg.fillAmount = 1;
       comboText.enabled = false;
       comboAddText.enabled = false;
       start = GetComponent<startScript>();
       player = GameObject.Find("Player").GetComponent<Tornado>();
       //dstr.SetActive(false);
       //dstr2.SetActive(false);
       if (!start.test)
       {
          dstr.SetActive(false);
          dstr2.SetActive(false);
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
            ScreenShot.TakeScreenshot_Static(500, 500);
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
                if (comboNum > 1)
                {
                    comboAdd = comboNum * 100;
                    StartCoroutine(showCombo());
                }
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
        //Show diasters after 3 minutes
        if(minutes <= 2 && !diasterOn)
        {
            diasterOn = true;
            StartCoroutine(Diasters());
        }
    }
    public IEnumerator showCombo()
    {
        comboAddText.text = "+ " + comboAdd.ToString();
        comboAddText.enabled = true;
        player.cmbScore += comboAdd;
        yield return new WaitForSeconds(1f);
        comboAddText.enabled = false;
    }
    public IEnumerator Diasters()
    {

        while (diasterOn)
        {
            dstr.SetActive(true);
            dstr2.SetActive(false);
            yield return new WaitForSeconds(15f);
            dstr.SetActive(false);
            dstr2.SetActive(true);
            yield return new WaitForSeconds(15f);
        }
    }

}

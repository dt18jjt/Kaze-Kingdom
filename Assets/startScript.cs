using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{
    public bool playing = false, keyPress = false, main = true;
    public Transform Rotator;
    public Camera startCam, PlayCam;
    public GameObject fadeIn, fadeOut, startImage, startText, timer;
    public AudioSource startAudio, playAudio;

    // Start is called before the first frame update
    void Start()
    {
        startCam.enabled = true;
        PlayCam.enabled = false;
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);
        timer.SetActive(false);
        startImage.SetActive(false);
        startText.SetActive(false);
        //get all audio sources to play seperately
        AudioSource[] audios = GetComponents<AudioSource>();
        startAudio = audios[0];
        playAudio = audios[1];
        //Main menu load
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);

    }
    private void FixedUpdate()
    {
        //start camera rotation
        if (!playing)
            Rotator.Rotate(0, -1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //start the game on any key press
        if (Input.anyKey && !keyPress && !main)
        {
            StartCoroutine(beginFade());
            keyPress = true;
        }
            
    }
    public void levelStart()
    {
        Invoke("mainOff", 0.5f);
        //Remove Main menu
        SceneManager.UnloadSceneAsync("Main");
        startImage.SetActive(true);
        //blinking text at start
        StartCoroutine(textBlink());
    }
    void mainOff()
    {
        main = false;
    }
    IEnumerator beginFade()
    {
        fadeIn.SetActive(true); //start fade in
        startAudio.Play(); // play start audio
        yield return new WaitForSeconds(1.0f);
        startImage.SetActive(false); //images at the start disabled
        fadeIn.SetActive(false); 
        fadeOut.SetActive(true); // start fade out
        startCam.enabled = false; // disable starting camera
        PlayCam.enabled = true; // enable play camera
        timer.SetActive(true);
        yield return new WaitForSeconds(1.9f);
        playAudio.Play(); // play looping audio
        playing = true;
        fadeOut.SetActive(false);
    }
    IEnumerator textBlink()
    {
        while (!keyPress)
        {
            startText.SetActive(true);
            yield return new WaitForSeconds(.7f);
            startText.SetActive(false);
            yield return new WaitForSeconds(.7f);
        }
        
    }
}

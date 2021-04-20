using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScript : MonoBehaviour
{
    public bool play = false, keyPress = false;
    public Transform Rotator;
    public Camera startCam, PlayCam;
    public GameObject fadeIn, fadeOut, startImage, startText;
    public AudioSource startAudio, playAudio;

    // Start is called before the first frame update
    void Start()
    {
        startImage.SetActive(true);
        startCam.enabled = true;
        PlayCam.enabled = false;
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);
        //get all audio sources to play seperately
        AudioSource[] audios = GetComponents<AudioSource>();
        startAudio = audios[0];
        playAudio = audios[1];
        //blinking text at start
        StartCoroutine(textBlink());

    }
    private void FixedUpdate()
    {
        //start camera rotation
        if (!play)
            Rotator.Rotate(0, -1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //start the game on any key press
        if (Input.anyKey && !keyPress)
        {
            StartCoroutine(beginFade());
            keyPress = true;
        }
            
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
        yield return new WaitForSeconds(1.9f);
        playAudio.Play(); // play looping audio
        play = true;
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

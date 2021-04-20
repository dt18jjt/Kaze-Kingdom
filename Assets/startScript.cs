using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScript : MonoBehaviour
{
    public bool play = false;
    public Transform Rotator;
    public Camera startCam, PlayCam;
    public GameObject fadeIn, fadeOut;
    public AudioSource startAudio, playAudio;

    // Start is called before the first frame update
    void Start()
    {
        startCam.enabled = true;
        PlayCam.enabled = false;
        //GetComponent<AudioSource>().enabled = false;
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);
        AudioSource[] audios = GetComponents<AudioSource>();
        startAudio = audios[0];
        playAudio = audios[1];
    }
    private void FixedUpdate()
    {
        if (!play)
            Rotator.Rotate(0, -1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            StartCoroutine(beginFade());
    }
    IEnumerator beginFade()
    {
        fadeIn.SetActive(true);
        startAudio.Play();
        yield return new WaitForSeconds(1.0f);
        fadeIn.SetActive(false);
        fadeOut.SetActive(true);
        startCam.enabled = false;
        PlayCam.enabled = true;
        yield return new WaitForSeconds(1.8f);
        //GetComponent<AudioSource>().enabled = true;
        playAudio.Play();
        play = true;
        fadeOut.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    Global global;
    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("G").GetComponent<Global>();
    }
    public void resume()
    {
        global.paused = false;
        SceneManager.UnloadSceneAsync("Pause");
    }
    public void quit()
    {
        SceneManager.LoadScene("Level");
        PlayerPrefs.DeleteAll();
    }

}

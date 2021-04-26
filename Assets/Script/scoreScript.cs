using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public float colScore, cmbScore, bScore, tScore;
    public Text colText, cmbText, tolText;
    // Start is called before the first frame update
    void Start()
    {
        colScore = PlayerPrefs.GetFloat("Score");
        cmbScore = PlayerPrefs.GetFloat("Combo");
        tScore = colScore + cmbScore;
        colText.text = colScore.ToString();
        cmbText.text = cmbScore.ToString();
        tolText.text = tScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void again()
    {
        SceneManager.LoadScene("Level");
        PlayerPrefs.DeleteAll();
    }
}

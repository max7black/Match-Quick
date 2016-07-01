using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{

    Text highScoreText;
    string highScoreKey = "HighScore";

    // Use this for initialization
    void Start()
    {
        // Displays the High Score on the screen
        highScoreText = GetComponent<Text>();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(highScoreKey, 0);
    }
}


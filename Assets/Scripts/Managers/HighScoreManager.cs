using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{

    Text highScoreText;
    string highScoreKey1 = "HighScoreGame1";  // the "Key" used to retrieve our High Score for game1
    string highScoreKey2 = "HighScoreGame2"; // the "Key" used to retireve our High Score for game2

    // Use this for initialization
    void Start()
    {
        // Displays the High Score on the screen
        highScoreText = GetComponent<Text>();
        if (ScoreManager.playingGame1)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt(highScoreKey1, 0);
        }
        else
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt(highScoreKey2, 0);
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

    public static int score = 0;        // initialize score to 0
    public static int finalScore = 0;
    public int highscore;               // Where the high score will be loaded to from PlayerPrefs
    string highScoreKey1 = "HighScoreGame1";  // the "Key" used to retrieve our High Score for game1
    string highScoreKey2 = "HighScoreGame2"; // the "Key" used to retireve our High Score for game2
    public static bool playingGame1 = true;               // Used to tell which game mode the High score is needed for
    
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

        // Get the high score from player prefs, and if there is none then set it to 0.
        if (playingGame1)
        {
            highscore = PlayerPrefs.GetInt(highScoreKey1, 0);
        }
        else
        {
            highscore = PlayerPrefs.GetInt(highScoreKey2, 0);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score;
    }

    void OnDisable()
    {
        finalScore = score;
        // If our score is greater than the highscore, then set highscore to score and save
        if (score > highscore)
        {
            if (playingGame1)
            {
                PlayerPrefs.SetInt(highScoreKey1, score);
            }
            else
            {
                PlayerPrefs.SetInt(highScoreKey2, score);
            }
            PlayerPrefs.Save();
        }
    }
}

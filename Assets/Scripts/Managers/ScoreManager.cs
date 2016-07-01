using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

    public static int score = 0;        // initialize score to 0
    public static int finalScore = 0;
    public int highscore;               // Where the high score will be loaded to from PlayerPrefs
    string highScoreKey = "HighScore";  // the "Key" used to retrieve our High Score
    
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

        // Get the high score from player prefs, and if there is none then set it to 0.
        highscore = PlayerPrefs.GetInt(highScoreKey, 0);    
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
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}

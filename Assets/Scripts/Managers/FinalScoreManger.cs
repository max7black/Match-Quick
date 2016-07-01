using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreManger : MonoBehaviour
{

    Text finalScoreText;

    // Use this for initialization
    void Start()
    {
        // Displays the Final Score on the screen
        finalScoreText = GetComponent<Text>();
        finalScoreText.text = "Final Score: " + ScoreManager.finalScore;
    }
}


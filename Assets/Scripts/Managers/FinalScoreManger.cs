using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreManger : MonoBehaviour
{

    public static int score;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Final Score: " + ScoreManager.score;
    }
}


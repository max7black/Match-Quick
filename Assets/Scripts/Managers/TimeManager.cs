using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

    public static float timeLeft;
    public static float lastMatchTime;
    Text text;
    Scene scene;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        timeLeft = 20.00f;  // initialize score to 0
        lastMatchTime = 20.0f; // intialize last match time to 0
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "Time Left: " + timeLeft.ToString("F2");
        if (timeLeft < 0)
       {
            SceneManager.LoadScene("GameOver");
       }
    }
}

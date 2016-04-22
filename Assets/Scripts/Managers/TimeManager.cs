using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {

    public float timeLeft;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        timeLeft = 5.00f;  // initialize score to 0
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "Time Left: " + timeLeft.ToString("F2");
//        if (timeLeft < 0)
//        {
//            Application.LoadLevel("gameOver");
//        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        ScoreManager.score = 0;             // reset the score back to 0 for the next game
        SceneManager.LoadScene("LevelSelect");
    }
}

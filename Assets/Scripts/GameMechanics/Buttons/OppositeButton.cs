using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OppositeButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        // Loads the scene for the game mode #2 "Opposite mode"
        SceneManager.LoadScene("In-game2");
    }
}
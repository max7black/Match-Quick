using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StandardButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene("In-game");
    }
}
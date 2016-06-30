using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OppositeButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene("In-game2");
    }
}
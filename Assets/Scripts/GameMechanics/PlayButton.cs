using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayButton : MonoBehaviour
{

    // Use this for initialization
    public void ButtonClicked()
    {
        SceneManager.LoadScene("In-game");
    }

}

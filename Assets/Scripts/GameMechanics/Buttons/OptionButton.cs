using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class OptionButton : MonoBehaviour
{

    // Use this for initialization
    public void ButtonClicked()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

}

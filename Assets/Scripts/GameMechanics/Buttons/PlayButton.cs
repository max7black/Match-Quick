﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}

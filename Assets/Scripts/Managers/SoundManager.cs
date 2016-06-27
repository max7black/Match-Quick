using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static float hSliderValue = 1.0f;

    void OnGUI()
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue, 0.0f, 1.0f);
    }

}

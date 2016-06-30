using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static float hSliderValue = 1.0f;
    public const float DefaultVolume = 1.0f;
    public Slider volumeSlider;

    void Start()
    {
        GameObject temp = GameObject.Find("VolumeSlider");
        if (temp != null)
        {
            volumeSlider = temp.GetComponent<Slider>();

            if (volumeSlider != null)
            {

                volumeSlider.normalizedValue = PlayerPrefs.HasKey("VolumeLevel") ? PlayerPrefs.GetFloat("VolumeLevel") : DefaultVolume;
            }
            else
            {
                Debug.LogError(temp.name + "- Does not contain a Slider component!");
            }
        }
        else
        {
            Debug.LogError("Couldn't find Gameobject called Volume Slider!");
        }
    }

    void Update()
    {
        hSliderValue = volumeSlider.value;
    }
}

    



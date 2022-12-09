using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InstructionMenu : MonoBehaviour
{
    public GameObject startPage;
    public GameObject settingsPage;

    public float exerciseDuration = 100f;
    public float minDuration = 100f;
    public float maxDuration = 600f;
    public PinchSlider durationSlider;
    public TextMeshPro durationSliderText;

    //void Start()
    //{
    //    startPage.SetActive(true);
    //    settingsPage.SetActive(false);
    //}

    //public void CloseMenu()
    //{
    //    startPage.SetActive(false);
    //    settingsPage.SetActive(false);
    //}

    //public void SwitchMenu()
    //{
    //    if (startPage.activeSelf) { 
    //        startPage.SetActive(false);
    //        settingsPage.SetActive(true);
    //    }
    //    else if(settingsPage.activeSelf)
    //    {
    //        startPage.SetActive(true);
    //        settingsPage.SetActive(false);
    //    }
    //}

    public void UpdateExerciseDuration()
    {
        exerciseDuration = Mathf.Lerp(minDuration, maxDuration, durationSlider.SliderValue);
        durationSliderText.text = "Duration: " + Mathf.FloorToInt(exerciseDuration).ToString();
    }
}

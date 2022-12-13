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

    public void UpdateExerciseDuration()
    {
        exerciseDuration = Mathf.Lerp(minDuration, maxDuration, durationSlider.SliderValue);
        durationSliderText.text = "Duration: " + Mathf.FloorToInt(exerciseDuration).ToString() + " s";
    }
}

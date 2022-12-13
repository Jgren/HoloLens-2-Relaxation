using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InstructionMenu : MonoBehaviour
{
    public GameObject startPage;
    public GameObject settingsPage;

    [System.Serializable]
    public struct DurationInfo
    {
        public float duration;
        public float minDuration;
        public float maxDuration;
        public PinchSlider durationSlider;
        public TextMeshPro durationSliderText;

        public void UpdateDuration(string description)
        {
            duration = Mathf.Lerp(minDuration, maxDuration, durationSlider.SliderValue);
            durationSliderText.text = description + " duration: " + Mathf.FloorToInt(duration).ToString() + " s";
        }
    }

    public DurationInfo exerciseDurationInfo;

    public void UpdateExerciseDuration()
    {
        exerciseDurationInfo.UpdateDuration("Duration");
    }
}

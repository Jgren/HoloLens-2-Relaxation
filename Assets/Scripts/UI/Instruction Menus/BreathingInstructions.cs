using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BreathingInstructions : InstructionMenu
{

    public Breathing breathingExercise;

    private float inhaleDuration = 3f;
    private float minInhale = 3f;
    private float maxInhale = 7f;
    public PinchSlider inhaleDurationSlider;
    public TextMeshPro inhaleDurationSliderText;

    private float inhaleHoldDuration = 3f;
    private float minInhaleHold = 1f;
    private float maxInhaleHold = 5f;
    public PinchSlider inhaleHoldSlider;
    public TextMeshPro inhaleHoldSliderText;

    private float exhaleDuration = 5f;
    private float minExhale = 3f;
    private float maxExhale = 7f;
    public PinchSlider exhaleDurationSlider;
    public TextMeshPro exhaleDurationSliderText;

    private float exhaleHoldDuration = 3f;
    private float minExhaleHold = 1f;
    private float maxExhaleHold = 5f;
    public PinchSlider exhaleHoldSlider;
    public TextMeshPro exhaleHoldSliderText;

    public void UpdateInhaleDuration()
    {
        inhaleDuration = Mathf.Lerp(minInhale, maxInhale, inhaleDurationSlider.SliderValue);
        inhaleDurationSliderText.text = "Inhale duration: " + Mathf.FloorToInt(inhaleDuration).ToString();
    }

    public void UpdateInhaleHoldDuration()
    {
        inhaleHoldDuration = Mathf.Lerp(minInhaleHold, maxInhaleHold, inhaleHoldSlider.SliderValue);
        inhaleHoldSliderText.text = "Inhale hold duration: " + Mathf.FloorToInt(inhaleHoldDuration).ToString();
    }

    public void UpdateExhaleDuration() 
    {
        exhaleDuration = Mathf.Lerp(minExhale, maxExhale, exhaleDurationSlider.SliderValue);
        exhaleDurationSliderText.text = "Exhale duration: " + Mathf.FloorToInt(exhaleDuration).ToString();
    }

    public void UpdateExhaleHoldDuration()
    {
        exhaleHoldDuration = Mathf.Lerp(minExhaleHold, maxExhaleHold, exhaleHoldSlider.SliderValue);
        exhaleHoldSliderText.text = "Exhale hold duration: " + Mathf.FloorToInt(exhaleHoldDuration).ToString();
    }

    public void StartExercise()
    {
        breathingExercise.inhaleTransitionDuration = inhaleDuration;
        breathingExercise.inhaleHoldDuration = inhaleHoldDuration;
        breathingExercise.exhaleTransitionDuration = exhaleDuration;
        breathingExercise.exhaleHoldDuration = exhaleHoldDuration;
        breathingExercise.duration = exerciseDuration;

        UIHandler.Instance.StartExercise(breathingExercise);
    }
}

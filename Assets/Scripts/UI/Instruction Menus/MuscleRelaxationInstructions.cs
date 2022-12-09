using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuscleRelaxationInstructions : InstructionMenu
{
    public MuscleRelaxation muscleRelaxationExercise;

    private float flexDuration = 10f;
    private float minFlexDur = 5f;
    private float maxFlexDur = 20f;
    public PinchSlider flexDurationSlider;
    public TextMeshPro flexDurationSliderText;

    private float relaxDuration = 10f;
    private float minRelaxDur = 5f;
    private float maxRelaxDur = 20f;
    public PinchSlider relaxDurationSlider;
    public TextMeshPro relaxDurationSliderText;

    public void UpdateFlexDuration()
    {
        flexDuration = Mathf.Lerp(minFlexDur, maxFlexDur, flexDurationSlider.SliderValue);
        flexDurationSliderText.text = "Flex Duration: " + Mathf.FloorToInt(flexDuration).ToString();
    }

    public void UpdateRelaxDuration()
    {
        relaxDuration = Mathf.Lerp(minRelaxDur, maxRelaxDur, relaxDurationSlider.SliderValue);
        relaxDurationSliderText.text = "Relax Duration: " + Mathf.FloorToInt(relaxDuration).ToString();
    }

    public void StartExercise()
    {
        muscleRelaxationExercise.flexHoldDuration = flexDuration;
        muscleRelaxationExercise.relaxHoldDuration = relaxDuration;
        muscleRelaxationExercise.duration = exerciseDuration;

        UIHandler.Instance.StartExercise(muscleRelaxationExercise);
    }
}

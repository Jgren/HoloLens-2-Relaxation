using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuscleRelaxationInstructions : InstructionMenu
{
    public MuscleRelaxationScript muscleRelaxationScript;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //UIHandler.Instance.exerciseDuration = exerciseDuration;
        UIHandler.Instance.StartExercise();

        muscleRelaxationScript.flexHoldDuration = flexDuration;
        muscleRelaxationScript.relaxHoldDuration = relaxDuration;
        muscleRelaxationScript.duration = exerciseDuration;
    }
}

using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BreathingInstructions : InstructionMenu
{

    public Breathing breathingExercise;

    public DurationInfo inhaleDurationInfo;
    public DurationInfo inhaleHoldDurationInfo;
    public DurationInfo exhaleDurationInfo;
    public DurationInfo exhaleHoldDurationInfo;

    public void UpdateInhaleDuration()
    {
        inhaleDurationInfo.UpdateDuration("Inhale");
    }

    public void UpdateInhaleHoldDuration()
    {
        inhaleHoldDurationInfo.UpdateDuration("Inhale hold");
    }

    public void UpdateExhaleDuration() 
    {
        exhaleDurationInfo.UpdateDuration("Exhale");
    }

    public void UpdateExhaleHoldDuration()
    {
        exhaleHoldDurationInfo.UpdateDuration("Exhale hold");
    }

    public void StartExercise()
    {
        // transfer slider values to exercise instance
        breathingExercise.inhaleTransitionDuration = inhaleDurationInfo.duration;
        breathingExercise.inhaleHoldDuration = inhaleHoldDurationInfo.duration;
        breathingExercise.exhaleTransitionDuration = exhaleDurationInfo.duration;
        breathingExercise.exhaleHoldDuration = exhaleHoldDurationInfo.duration;
        breathingExercise.duration = exerciseDurationInfo.duration;

        UIHandler.Instance.StartExercise(breathingExercise);
    }
}

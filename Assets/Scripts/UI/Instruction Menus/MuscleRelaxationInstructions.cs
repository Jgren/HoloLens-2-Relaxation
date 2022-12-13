using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuscleRelaxationInstructions : InstructionMenu
{
    public MuscleRelaxation muscleRelaxationExercise;

    public DurationInfo flexDurationInfo;
    public DurationInfo relaxDurationInfo;

    public void UpdateFlexDuration()
    {
        flexDurationInfo.UpdateDuration("Flex duration");
    }

    public void UpdateRelaxDuration()
    {
        relaxDurationInfo.UpdateDuration("Relax duration");
    }

    public void StartExercise()
    {
        // transfer slider values to exercise instance
        muscleRelaxationExercise.flexHoldDuration = flexDurationInfo.duration;
        muscleRelaxationExercise.relaxHoldDuration = relaxDurationInfo.duration;
        muscleRelaxationExercise.duration = exerciseDurationInfo.duration;

        UIHandler.Instance.StartExercise(muscleRelaxationExercise);
    }
}

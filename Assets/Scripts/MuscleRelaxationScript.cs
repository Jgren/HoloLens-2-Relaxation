using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MuscleRelaxationScript : ExerciseBaseScript
{
    public List<GameObject> muscles;
    public float flexTranstitionDuration = 0.5f;
    public float flexHoldDuration = 2f;
    public float relaxTransitionDuration = 0.5f;
    public float relaxHoldDuration = 2f;

    private float muscleStateTimer = 0f;
    private MuscleState muscleState = MuscleState.FlexTransition;
    private int muscleIndex = 0;

    private enum MuscleState
    {
        FlexTransition,
        FlexHold,
        RelaxTransition,
        RelaxHold
    }

    private void AnimateMuscles(float transitionDuration, Color from, Color to)
    {
        float transition = muscleStateTimer / transitionDuration;
        foreach (Renderer rend in muscles[muscleIndex].GetComponentsInChildren<Renderer>())
        {
            rend.material.color = Color.Lerp(from, to, transition);
        }
    }

    private void UpdateMuscleState()
    {
        muscleStateTimer += Time.deltaTime;

        switch (muscleState)
        {
            case MuscleState.FlexTransition:
                AnimateMuscles(flexTranstitionDuration, Color.white, Color.red);
                if(muscleStateTimer >= flexTranstitionDuration)
                {
                    muscleStateTimer = 0f;
                    muscleState = MuscleState.FlexHold;
                }
                break;
            case MuscleState.FlexHold:
                if (muscleStateTimer >= flexHoldDuration)
                {
                    muscleStateTimer = 0f;
                    muscleState = MuscleState.RelaxTransition;
                }
                break;
            case MuscleState.RelaxTransition:
                AnimateMuscles(relaxTransitionDuration, Color.red, Color.white);
                if (muscleStateTimer >= relaxTransitionDuration)
                {
                    muscleStateTimer = 0f;
                    muscleState = MuscleState.RelaxHold;
                }
                break;
            case MuscleState.RelaxHold:
                if (muscleStateTimer >= relaxHoldDuration)
                {
                    muscleStateTimer = 0f;
                    muscleState = MuscleState.FlexTransition;
                    muscleIndex = (muscleIndex + 1) % muscles.Count;
                }
                break;
        }
    }


    void Update()
    {
        UpdateMuscleState();        
    }
}

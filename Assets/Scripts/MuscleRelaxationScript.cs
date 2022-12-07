using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MuscleRelaxationScript : ExerciseBaseScript
{
    public List<MusclePair> muscles;
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

    [System.Serializable]
    public struct MusclePair
    {
        public GameObject left;
        public GameObject right;
    }

    public override void OnExit()
    {
        muscleStateTimer = 0f;
        AnimateMuscles(1f, new Color(1f,1f,1f), new Color(1f,0f,0f));
        muscleState = MuscleState.FlexTransition;
        muscleIndex = 0;
    }

    private void AnimateMuscles(float transitionDuration, Color from, Color to)
    {
        float transition = muscleStateTimer / transitionDuration;
        MusclePair pair = muscles[muscleIndex];
        Color lerpedColor = Color.Lerp(from, to, transition);

        pair.left.GetComponent<Renderer>().material.color = lerpedColor;
        pair.right.GetComponent<Renderer>().material.color = lerpedColor;
    }

    private void UpdateMuscleState()
    {
        muscleStateTimer += Time.deltaTime;

        switch (muscleState)
        {
            case MuscleState.FlexTransition:
                AnimateMuscles(flexTranstitionDuration, new Color(1f,1f,1f), new Color(1f, 0f, 0f));
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
                AnimateMuscles(relaxTransitionDuration, new Color(1f, 0f, 0), new Color(1f, 1f, 1f));
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
        base.CheckTimer();
        UpdateMuscleState();        
    }
}

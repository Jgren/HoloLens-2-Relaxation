using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MuscleRelaxation : Exercise
{
    public List<MusclePair> muscles;
    public MusclePair restOfBody;
    public float flexTranstitionDuration = 0.5f;
    public float flexHoldDuration = 2f;
    public float relaxTransitionDuration = 0.5f;
    public float relaxHoldDuration = 2f;

    private float muscleStateTimer = 0f;
    private MuscleState muscleState = MuscleState.FlexTransition;
    private int muscleIndex = 0;

    private enum MuscleState
    {
        FlexTransition, // The process of tensing the muscle from 0% to 100%
        FlexHold, // Holding the tensed position
        RelaxTransition, //The process of releasing the tension in the muscle from 100% to 0%
        RelaxHold // Relaxing your body and switching what muscle to tense
    }

    [System.Serializable]
    public struct MusclePair
    {
        public GameObject left;
        public GameObject right;
    }

    // Called when exiting the exercise
    public override void OnExit()
    {
        muscleStateTimer = 0f;
        AnimateMuscles(1f, new Color(1f,1f,1f), new Color(1f,0f,0f));
        SetBodyRelaxation(1f);
        muscleState = MuscleState.FlexTransition;
        muscleIndex = 0;
    }

    private void AnimateMuscles(float transitionDuration, Color from, Color to)
    {
        // Animate the muscles by making the switch colors; white to red when tensing, red to white when releasing tension
        float transition = muscleStateTimer / transitionDuration;
        MusclePair pair = muscles[muscleIndex];
        Color lerpedColor = Color.Lerp(from, to, transition); // "transition" is a value 0-1 which determins the mix of colors. 0 is 100% the color "from" and 1 is 100% the color "to".

        pair.left.GetComponent<Renderer>().material.color = lerpedColor;
        pair.right.GetComponent<Renderer>().material.color = lerpedColor;
    }

    private void SetBodyRelaxation(float transitionDuration)
    {
        float progression = Mathf.Sin(muscleStateTimer / transitionDuration * Mathf.PI);
        //Set the variable _RelaxProgression for both characters
        for(int i=0; i<muscles.Count; i++)
        {
            muscles[i].left.GetComponent<Renderer>().material.SetFloat("_RelaxProgression", progression);
            muscles[i].right.GetComponent<Renderer>().material.SetFloat("_RelaxProgression", progression);
        }

        restOfBody.left.GetComponent<Renderer>().material.SetFloat("_RelaxProgression", progression);
        restOfBody.right.GetComponent<Renderer>().material.SetFloat("_RelaxProgression", progression);
    }

    private void UpdateMuscleState()
    {
        muscleStateTimer += Time.deltaTime;
        // State machine to distinguish what part of the program is active
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
                SetBodyRelaxation(relaxHoldDuration);
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

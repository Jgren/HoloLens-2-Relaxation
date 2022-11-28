using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingScript : ExerciseBaseScript
{
    public float inhaleDuration;
    public float inhaleHoldDuration;
    public float exhaleDuration;
    public float exhaleHoldDuration;

    public float maxScale = 2;
    private Vector3 currentScale = Vector3.one;

    public enum breathState
    {
        breathingIn,
        holdingBreath,
        breathingOut
    }
    public breathState state = breathState.breathingIn;
    public float breathStateTimer;

    public GameObject lungs;
    public ParticleSystem airParticlesIn;
    public ParticleSystem airParticlesOut;

    private void Update()
    {
        UpdateBreathState();
    }

    void UpdateBreathState()
    {
        switch (state)
        {
            case breathState.breathingIn:
                Debug.Log("breathing in");
                currentScale += new Vector3(0.01f, 0.01f, 0.01f);
                if (currentScale.x > maxScale)
                    state = breathState.holdingBreath;
                lungs.transform.localScale = currentScale;
                break;
            case breathState.holdingBreath:
                Debug.Log("holding breath");
                breathStateTimer += Time.deltaTime;
                if (breathStateTimer > 1)
                    state = breathState.breathingOut;
                break;
            case breathState.breathingOut:
                Debug.Log("breathing out");
                currentScale -= new Vector3(0.01f, 0.01f, 0.01f);
                if (currentScale.x <= 1)
                    state = breathState.breathingIn;
                lungs.transform.localScale = currentScale;
                break;

        }
    }

    void AnimateLungs()
    {
        ;
    }
}

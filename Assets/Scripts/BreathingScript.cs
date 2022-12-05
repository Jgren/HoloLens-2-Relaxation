using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BreathingScript : ExerciseBaseScript
{
    public Animator animationController;
    public ParticleSystem airParticlesIn;
    public ParticleSystem airParticlesOut;
    public float inhaleTransitionDuration;
    public float inhaleHoldDuration;
    public float exhaleTransitionDuration;
    public float exhaleHoldDuration;

    private float breathStateTimer = 0f;
    private BreathState breathState = BreathState.InhaleTransition;

    private enum BreathState
    {
        InhaleTransition,
        InhaleHold,
        ExhaleTransition,
        ExhaleHold
    }

    private void AnimateLungs(float transitionDuration, bool inhale)
    {
        float transition = breathStateTimer / transitionDuration;
        if (!inhale)
        {
            transition = 1f - transition;
        }
        animationController.SetFloat("Expanded", transition);
    }

    void UpdateBreathState()
    {
        breathStateTimer += Time.deltaTime;

        switch (breathState)
        {
            case BreathState.InhaleTransition:
                if (!airParticlesIn.isPlaying)
                {
                    airParticlesIn.Play();
                }
                AnimateLungs(inhaleTransitionDuration, true);
                if(breathStateTimer >= inhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.InhaleHold;
                    airParticlesIn.Stop();
                }
                break;
            case BreathState.InhaleHold:
                if(breathStateTimer >= inhaleHoldDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.ExhaleTransition;
                }
                break;
            case BreathState.ExhaleTransition:
                if (!airParticlesOut.isPlaying)
                {
                    airParticlesOut.Play();
                }
                AnimateLungs(exhaleTransitionDuration, false);
                if(breathStateTimer >= exhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.ExhaleHold;
                    airParticlesOut.Stop();
                }
                break;
            case BreathState.ExhaleHold:
                if (breathStateTimer >= exhaleHoldDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.InhaleTransition;
                }
                break;
        }
    }

    private void Update()
    {
        UpdateBreathState();
    }
}

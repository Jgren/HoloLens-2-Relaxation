using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Breathing : Exercise
{
    public Animator animationController;
    public ParticleSystem airParticlesIn;
    public ParticleSystem airParticlesOut;
    public Material lungMaterial;
    public AudioSource inhaleSound;
    public AudioSource exhaleSound;
    [HideInInspector] public float inhaleTransitionDuration;
    [HideInInspector] public float inhaleHoldDuration;
    [HideInInspector] public float exhaleTransitionDuration;
    [HideInInspector] public float exhaleHoldDuration;

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
        lungMaterial.SetFloat("_Expanded", transition);
    }

    public override void OnExit()
    {
        breathStateTimer = 0f;
        breathState = BreathState.InhaleTransition;
    }

    private void UpdateBreathState()
    {
        breathStateTimer += Time.deltaTime;

        // check breath state and perform different events for the different stages
        switch (breathState)
        {
            case BreathState.InhaleTransition:
                // first time
                if (!inhaleSound.isPlaying)
                {
                    airParticlesIn.Play();
                    inhaleSound.pitch = inhaleSound.clip.length / inhaleTransitionDuration;
                    inhaleSound.Play();
                }
                // always
                AnimateLungs(inhaleTransitionDuration, true);
                // last time
                if(breathStateTimer >= inhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.InhaleHold;
                    airParticlesIn.Stop();
                    inhaleSound.Stop();
                }
                break;

            case BreathState.InhaleHold:
                // last time
                if(breathStateTimer >= inhaleHoldDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.ExhaleTransition;
                }
                break;

            case BreathState.ExhaleTransition:
                // fist time
                if (!exhaleSound.isPlaying)
                {
                    airParticlesOut.Play();
                    exhaleSound.pitch = exhaleSound.clip.length / exhaleTransitionDuration;
                    exhaleSound.Play();
                }
                // always
                AnimateLungs(exhaleTransitionDuration, false);
                // last time
                if(breathStateTimer >= exhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.ExhaleHold;
                    airParticlesOut.Stop();
                    exhaleSound.Stop();
                }
                break;

            case BreathState.ExhaleHold:
                // last time
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
        base.CheckTimer();
        UpdateBreathState();
    }
}

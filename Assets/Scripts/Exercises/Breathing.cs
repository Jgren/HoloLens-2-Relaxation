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
        lungMaterial.SetFloat("_Expanded", transition);
    }

    public override void OnExit()
    {
        breathStateTimer = 0f;
        breathState = BreathState.InhaleTransition;
    }

    void UpdateBreathState()
    {
        breathStateTimer += Time.deltaTime;

        switch (breathState)
        {
            case BreathState.InhaleTransition:
                if (!inhaleSound.isPlaying)// first time
                {
                    airParticlesIn.Play();
                    inhaleSound.pitch = inhaleSound.clip.length / inhaleTransitionDuration;
                    inhaleSound.Play();
                }
                AnimateLungs(inhaleTransitionDuration, true);
                if(breathStateTimer >= inhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.InhaleHold;
                    airParticlesIn.Stop();
                    inhaleSound.Stop();
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
                if (!exhaleSound.isPlaying) // first time
                {
                    airParticlesOut.Play();
                    exhaleSound.pitch = exhaleSound.clip.length / exhaleTransitionDuration;
                    exhaleSound.Play();
                }
                AnimateLungs(exhaleTransitionDuration, false);
                if(breathStateTimer >= exhaleTransitionDuration)
                {
                    breathStateTimer = 0f;
                    breathState = BreathState.ExhaleHold;
                    airParticlesOut.Stop();
                    exhaleSound.Stop();
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
        base.CheckTimer();
        UpdateBreathState();
    }
}

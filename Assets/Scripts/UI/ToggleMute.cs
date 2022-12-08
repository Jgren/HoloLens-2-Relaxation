using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMute : MonoBehaviour
{
    public AudioClip summerBirds;
    public AudioClip rainAndBirds;
    public AudioClip beachWaves;

    public AudioSource source;

    private enum TrackState
    {
        mute,
        summerBirds,
        rainAndBirds,
        beachWaves
    }
    private TrackState trackState = TrackState.mute;

    private void Start()
    {
        source.loop = true;
    }

    public void CycleTrack()
    {
        switch (trackState)
        {
            case TrackState.mute:
                trackState = TrackState.summerBirds;
                source.volume = 1;
                source.clip = summerBirds;
                source.Play();
                break;
            case TrackState.summerBirds:
                trackState = TrackState.rainAndBirds;
                source.volume = 1;
                source.clip = rainAndBirds;
                source.Play();
                break;
            case TrackState.rainAndBirds:
                trackState = TrackState.beachWaves;
                source.volume = 1;
                source.clip = beachWaves;
                source.Play();
                break;
            case TrackState.beachWaves:
                trackState = TrackState.mute;
                source.volume = 0;
                break;
        }
    }
}

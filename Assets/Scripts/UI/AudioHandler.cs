using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioClip summerBirds;
    public AudioClip rainAndBirds;
    public AudioClip beachWaves;

    public AudioSource source;

    private enum TrackState
    {
        Mute,
        SummerBirds,
        RainAndBirds,
        BeachWaves
    }
    private TrackState trackState = TrackState.Mute;

    private void Start()
    {
        source.loop = true;
    }

    public void CycleTrack()
    {
        switch (trackState)
        {
            case TrackState.Mute:
                trackState = TrackState.SummerBirds;
                source.volume = 1;
                source.clip = summerBirds;
                source.Play();
                break;
            case TrackState.SummerBirds:
                trackState = TrackState.RainAndBirds;
                source.clip = rainAndBirds;
                source.Play();
                break;
            case TrackState.RainAndBirds:
                trackState = TrackState.BeachWaves;
                source.clip = beachWaves;
                source.Play();
                break;
            case TrackState.BeachWaves:
                trackState = TrackState.Mute;
                source.volume = 0;
                break;
        }
    }
}

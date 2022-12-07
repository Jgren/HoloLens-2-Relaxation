using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBaseScript : MonoBehaviour
{
    public float duration;
    private float timer = 0f;
    
    public float TimeElapsed
    {
        get
        {
            return timer;
        }
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    public virtual void OnExit()
    {
        
    }

    protected void CheckTimer()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            UIHandler.Instance.QuitActiveExercise();
        }
    }
}

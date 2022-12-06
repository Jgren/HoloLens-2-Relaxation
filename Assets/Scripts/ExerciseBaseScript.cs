using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBaseScript : MonoBehaviour
{
    public float duration;
    private float timer = 0f;

    private void OnEnable()
    {
        timer = 0f;
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

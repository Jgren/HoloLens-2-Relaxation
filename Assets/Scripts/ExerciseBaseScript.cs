using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseBaseScript : MonoBehaviour
{
    public float duration;
    float timer = 0f;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void ExitExercise()
    {
        // call return to menu from ui class
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            ExitExercise();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MuscleRelaxationScript : ExerciseBaseScript
{
    public List<GameObject> muscles;
    
    enum MuscleState
    {
        Flex,
        Relax,
        Next
    }
    enum Muscle
    {
        Biceps,
        Quadriceps
    }
    MuscleState muscleState = MuscleState.Next;
    Muscle muscle;
    
    IEnumerator HoldFlex()
    {
        yield return new WaitForSeconds(3);
        muscleState = MuscleState.Relax;
        startTime = Time.time;
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(3);
        muscleState = MuscleState.Next;
        startTime = Time.time;
    }

    float startTime;
    void FlexAndRelax()
    {
        float t = (Time.time - startTime) / 3;
        if (muscleState == MuscleState.Flex)
        {
            foreach (Renderer rend in muscles[(int)muscle].GetComponentsInChildren<Renderer>())
                rend.material.color = Color.Lerp(Color.white, Color.red, t);

            if (t >= 1)
                StartCoroutine(HoldFlex());
        }
        if (muscleState == MuscleState.Relax)
        {
            foreach (Renderer rend in muscles[(int)muscle].GetComponentsInChildren<Renderer>())
                rend.material.color = Color.Lerp(Color.red, Color.white, t);

            if (t >= 1)
                StartCoroutine(Recharge());
        }
    }
    void StartFlex()
    {
        Array values = Enum.GetValues(typeof(Muscle));
        System.Random random = new System.Random();
        if (muscleState == MuscleState.Next)
        {
            muscle = (Muscle)values.GetValue(random.Next(values.Length));
            muscleState = MuscleState.Flex;
            startTime = Time.time;
        }
        FlexAndRelax();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartFlex();        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseButton : MonoBehaviour
{
    public UIHandler userInterface;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.enabled)
        {
            //userInterface.startExercise = true;
            this.enabled = false;
        }
    }
}

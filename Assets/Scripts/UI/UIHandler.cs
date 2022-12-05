using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public bool quitExercise = false;
    public bool startExercise = false;
    public GameObject mainMenu;
    public GameObject exerciseMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        exerciseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quitExercise)
        {
            exerciseMenu.SetActive(false);
            mainMenu.SetActive(true);
            quitExercise= false;
        }
        if(startExercise)
        {
            exerciseMenu.SetActive(true);
            mainMenu.SetActive(false);
            startExercise= false;
        }
    }
}

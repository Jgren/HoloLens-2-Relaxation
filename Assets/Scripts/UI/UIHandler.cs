using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private static UIHandler instance = null;
    public static UIHandler Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public List<ExerciseBaseScript> exercises;
    public GameObject mainMenu;
    public GameObject exerciseMenu;

    private ExerciseBaseScript activeExercise = null;
    private Dictionary<string, ExerciseBaseScript> nameToExercise = new Dictionary<string, ExerciseBaseScript>();
    
    private void ToggleMenus(bool activateMainMenu)
    {
        mainMenu.SetActive(activateMainMenu);
        exerciseMenu.SetActive(!activateMainMenu);
    }
    private void Start()
    {
        for(int i=0; i<exercises.Count; i++)
        {
            nameToExercise.Add(exercises[i].name, exercises[i]);
        }

        ToggleMenus(true);
    }
    public void StartExercise(string exerciseName)
    {
        if (nameToExercise.TryGetValue(exerciseName, out ExerciseBaseScript exercise))
        {
            exercise.gameObject.SetActive(true);
            activeExercise = exercise;

            ToggleMenus(false);
        }
    }
    public void QuitActiveExercise()
    {
        if(activeExercise != null)
        {
            activeExercise.gameObject.SetActive(false);
            activeExercise = null;

            ToggleMenus(true);
        }
    }
}

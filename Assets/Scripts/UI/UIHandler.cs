using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

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

    private enum MenuState
    {
        main,
        instruction,
        exercise,
    }

    public List<ExerciseBaseScript> exercises;
    public List<GameObject> instructionMenus;
    public GameObject mainMenu;
    public GameObject exerciseMenu;
    public GameObject instructionMenu;
    public Transform exerciseProgressionBar;



    private string activeExerciseName;
    private ExerciseBaseScript activeExercise = null;
    private Dictionary<string, ExerciseBaseScript> nameToExercise = new Dictionary<string, ExerciseBaseScript>();
    private Dictionary<string, GameObject> nameToInstructionMenu = new Dictionary<string, GameObject>();
    public float exerciseDuration = 0f;
    private Vector3 initProgressionBarScale;
    private Vector3 initProgressionBarPos;
    
    private void ToggleMenus(bool activateMainMenu)
    {
        mainMenu.SetActive(activateMainMenu);
        exerciseMenu.SetActive(!activateMainMenu);
    }

    private void ToggleMenus(MenuState menuToOpen)
    {
        switch(menuToOpen)
        {
            case MenuState.main:
                mainMenu.SetActive(true);
                CloseInstructions();
                exerciseMenu.SetActive(false);
                break;
            case MenuState.instruction:
                mainMenu.SetActive(false);
                instructionMenu.SetActive(true);
                exerciseMenu.SetActive(false);
                break;
            case MenuState.exercise:
                mainMenu.SetActive(false);
                CloseInstructions();
                exerciseMenu.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        for(int i=0; i<exercises.Count; i++)
        {
            nameToExercise.Add(exercises[i].name, exercises[i]);
            nameToInstructionMenu.Add(exercises[i].name, instructionMenus[i]);
        }


        initProgressionBarScale = exerciseProgressionBar.localScale;
        initProgressionBarPos = exerciseProgressionBar.localPosition;
        ToggleMenus(MenuState.main);
    }

    private void Update()
    {
        if(activeExercise != null)
        {
            float scaleX = activeExercise.TimeElapsed / activeExercise.duration * initProgressionBarScale.x;
            float posX = -initProgressionBarScale.x * 0.5f + scaleX * 0.5f;
            exerciseProgressionBar.localScale = new Vector3(scaleX, initProgressionBarScale.y, initProgressionBarScale.z);
            exerciseProgressionBar.localPosition = new Vector3(posX, initProgressionBarPos.x, initProgressionBarPos.z);
        }
    }

    public void OpenInstructions(string exerciseName)
    {
        activeExerciseName = exerciseName;
        if(nameToInstructionMenu.TryGetValue(activeExerciseName, out GameObject exerciseInstructionMenu))
        {
            ToggleMenus(MenuState.instruction);
            exerciseInstructionMenu.SetActive(true);
        }
    }

    private void CloseInstructions()
    {
        instructionMenu.SetActive(false);
        if (nameToInstructionMenu.TryGetValue(activeExerciseName, out GameObject exerciseInstructionMenu))
        {
            exerciseInstructionMenu.SetActive(false);
        }
    }

    public void StartExercise()
    {
        if (nameToExercise.TryGetValue(activeExerciseName, out ExerciseBaseScript exercise))
        {
            exercise.gameObject.SetActive(true);
            activeExercise = exercise;
            ToggleMenus(MenuState.exercise);
        }
    }
    public void QuitActiveExercise()
    {
        if(activeExercise != null)
        {
            activeExercise.OnExit();
            activeExercise.gameObject.SetActive(false);
            activeExercise = null;
            ToggleMenus(MenuState.main);
        }
    }
}

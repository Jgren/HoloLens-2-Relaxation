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

    public List<ExerciseBaseScript> exercises;
    public List<GameObject> instructionMenus;
    public GameObject mainMenu;
    public GameObject exerciseMenu;
    public GameObject instructionMenu;
    //public float minDuration = 100f;
    //public float maxDuration = 600f;
    //public PinchSlider durationSlider;
    //public TextMeshPro durationSliderText;
    public Transform exerciseProgressionBar;
    public TextMeshPro exerciseTimerText;
    //public TextMeshPro instructionText;


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

    private void ToggleMenus(string menuToOpen)
    {
        switch(menuToOpen)
        {
            case "main":
                mainMenu.SetActive(true);
                CloseInstructions();
                exerciseMenu.SetActive(false);
                break;
            case "instruction":
                mainMenu.SetActive(false);
                instructionMenu.SetActive(true);
                exerciseMenu.SetActive(false);
                break;
            case "exercise":
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

        //ToggleMenus(true);
        ToggleMenus("main");
        UpdateDuration();
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
            ToggleMenus("instruction");


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


    public void StartExercise(string exerciseName)
    {
        if (nameToExercise.TryGetValue(exerciseName, out ExerciseBaseScript exercise))
        {
            exercise.duration = exerciseDuration;
            exercise.gameObject.SetActive(true);
            activeExercise = exercise;

            //ToggleMenus(false);
            ToggleMenus("exercise");
        }
    }
    public void StartExercise()
    {
        if (nameToExercise.TryGetValue(activeExerciseName, out ExerciseBaseScript exercise))
        {
            //exercise.duration = exerciseDuration;
            exercise.gameObject.SetActive(true);
            activeExercise = exercise;

            //ToggleMenus(false);
            ToggleMenus("exercise");
        }
    }
    public void QuitActiveExercise()
    {
        if(activeExercise != null)
        {
            activeExercise.OnExit();
            activeExercise.gameObject.SetActive(false);
            activeExercise = null;

            //ToggleMenus(true);
            ToggleMenus("main");
        }
    }

    public void UpdateDuration()
    {
        //exerciseDuration = Mathf.Lerp(minDuration, maxDuration, durationSlider.SliderValue);
        //durationSliderText.text = "Duration: "+Mathf.FloorToInt(exerciseDuration).ToString();
    }

    //public void SetInstructionText(string inputText)
    //{
    //    instructionText.text = inputText.Replace("\\n", Environment.NewLine);
    //}

}

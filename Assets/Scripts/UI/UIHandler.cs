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

    public List<Exercise> exercises;
    public List<InstructionMenu> instructionMenus;
    public GameObject mainMenu;
    public GameObject instructionMenuWrapper;
    public GameObject exerciseMenu;
    public Transform exerciseProgressionBar;

    private Exercise activeExercise = null;
    private InstructionMenu activeInstructionMenu = null;
    private Dictionary<string, InstructionMenu> nameToInstructionMenu = new Dictionary<string, InstructionMenu>();
    private Vector3 initProgressionBarScale;
    private Vector3 initProgressionBarPos;

    private void Start()
    {
        for(int i=0; i<instructionMenus.Count; i++)
        {
            nameToInstructionMenu.Add(exercises[i].name, instructionMenus[i]);
        }


        initProgressionBarScale = exerciseProgressionBar.localScale;
        initProgressionBarPos = exerciseProgressionBar.localPosition;
        mainMenu.SetActive(true);
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

    public void OpenInstructionMenu(string exerciseName)
    {
        if(nameToInstructionMenu.TryGetValue(exerciseName, out InstructionMenu selectedInstructionMenu))
        {
            activeInstructionMenu = selectedInstructionMenu;
            mainMenu.SetActive(false);
            instructionMenuWrapper.SetActive(true);
            selectedInstructionMenu.gameObject.SetActive(true);
        }
    }

    public void ToggleInstructionMenuSettingsPage()
    {
        if(activeInstructionMenu != null)
        {
            bool isOnStartPage = activeInstructionMenu.startPage.activeSelf;
            activeInstructionMenu.startPage.SetActive(!isOnStartPage);
            activeInstructionMenu.settingsPage.SetActive(isOnStartPage);
        }
    }

    private void CloseActiveInstructionMenu()
    {
        if(activeInstructionMenu != null)
        {
            activeInstructionMenu.gameObject.SetActive(false);
            activeInstructionMenu = null;
        }
    }

    public void StartExercise(Exercise selectedExercise)
    {
        activeExercise = selectedExercise;
        CloseActiveInstructionMenu();
        instructionMenuWrapper.SetActive(false);
        selectedExercise.gameObject.SetActive(true);
        exerciseMenu.SetActive(true);
    }

    public void CloseActiveExercise()
    {
        if(activeExercise != null)
        {
            activeExercise.OnExit();
            activeExercise.gameObject.SetActive(false);
            activeExercise = null;
            exerciseMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}

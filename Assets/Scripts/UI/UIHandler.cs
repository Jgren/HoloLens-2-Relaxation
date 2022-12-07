using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float minDuration = 100f;
    public float maxDuration = 600f;
    public PinchSlider durationSlider;
    public TextMeshPro durationSliderText;
    public Transform exerciseProgressionBar;

    private ExerciseBaseScript activeExercise = null;
    private Dictionary<string, ExerciseBaseScript> nameToExercise = new Dictionary<string, ExerciseBaseScript>();
    private float exerciseDuration = 0f;
    private Vector3 initProgressionBarScale;
    private Vector3 initProgressionBarPos;
    
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

        initProgressionBarScale = exerciseProgressionBar.localScale;
        initProgressionBarPos = exerciseProgressionBar.localPosition;

        ToggleMenus(true);
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

    public void StartExercise(string exerciseName)
    {
        if (nameToExercise.TryGetValue(exerciseName, out ExerciseBaseScript exercise))
        {
            exercise.duration = exerciseDuration;
            exercise.gameObject.SetActive(true);
            activeExercise = exercise;

            ToggleMenus(false);
        }
    }
    public void QuitActiveExercise()
    {
        if(activeExercise != null)
        {
            activeExercise.OnExit();
            activeExercise.gameObject.SetActive(false);
            activeExercise = null;

            ToggleMenus(true);
        }
    }

    public void UpdateDuration()
    {
        exerciseDuration = Mathf.Lerp(minDuration, maxDuration, durationSlider.SliderValue);
        durationSliderText.text = "Duration: "+Mathf.FloorToInt(exerciseDuration).ToString();
    }
}

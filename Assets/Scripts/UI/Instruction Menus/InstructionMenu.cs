using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InstructionMenu : MonoBehaviour
{
    public GameObject startPage;
    public GameObject settingsPage;
    public UIHandler uIHandler;

    public float exerciseDuration;
    public float minDuration = 100f;
    public float maxDuration = 600f;
    public PinchSlider durationSlider;
    public TextMeshPro durationSliderText;

    // Start is called before the first frame update
    void Start()
    {
        startPage.SetActive(true);
        settingsPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        startPage.SetActive(false);
        settingsPage.SetActive(false);
    }

    public void SwitchMenu()
    {
        if (startPage.activeSelf) { 
            startPage.SetActive(false);
            settingsPage.SetActive(true);
        }
        else if(settingsPage.activeSelf)
        {
            startPage.SetActive(true);
            settingsPage.SetActive(false);
        }
    }


}

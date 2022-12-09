using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject target;

    public void Toggle()
    {
        target.SetActive(!target.activeSelf);
    }
}

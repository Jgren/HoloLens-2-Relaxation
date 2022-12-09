using UnityEngine;

public class Test_Lungs : MonoBehaviour
{
    public Animator animationController;
    public float speed = 1f;

    void Update()
    {
        animationController.SetFloat("Expanded", 0.5f + 0.5f*Mathf.Sin(Time.timeSinceLevelLoad * speed));
    }
}

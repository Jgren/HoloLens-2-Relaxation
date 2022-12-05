using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIPos : MonoBehaviour
{
    public GameObject self;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        self.transform.position = camera.transform.position;
        self.transform.Translate(0,0,0.03f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

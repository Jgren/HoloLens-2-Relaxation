using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Transform zombieTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        zombieTransform.Rotate(new Vector3(0, 1000 * Time.deltaTime, 0));
    }
}

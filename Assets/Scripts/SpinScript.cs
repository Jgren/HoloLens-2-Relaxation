using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Transform transform;

    void Update()
    {
        transform.Rotate(new Vector3(0, 1000 * Time.deltaTime, 0));
    }
}

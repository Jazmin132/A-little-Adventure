using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float _RayDist;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}

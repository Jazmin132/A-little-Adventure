using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] Transform _Player;
    [SerializeField] float _sensitivity = 2f;
    [SerializeField] float _distance;

    private float _currentX = 0f;
    private float _currentY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}

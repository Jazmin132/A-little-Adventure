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
    private void Update()
    {
        float H = Input.GetAxis("Mouse X") * _sensitivity;
        float V = Input.GetAxis("Mouse Y") * _sensitivity;

        _currentX += H;
        _currentY -= V;
        _currentY = Mathf.Clamp(_currentY, -5f, 33f);

        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);

        Vector3 position = rotation * new Vector3(0f, 0f, -_distance) + _Player.position;

        transform.LookAt(_Player.position);
        transform.rotation = rotation;
        transform.position = position;
    }
}

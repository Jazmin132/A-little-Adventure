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

    public void Aim()
    {
        Physics.Raycast(transform.position, Vector3.forward, _RayDist);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 X = new Vector3(_RayDist, _RayDist, _RayDist);
        //Gizmos.DrawLine(transform.position, transform.forward + X); ?? Arreglar
    }
}

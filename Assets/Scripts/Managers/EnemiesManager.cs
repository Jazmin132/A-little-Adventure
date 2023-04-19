using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;
    public PlayerM _target;
    public LayerMask WALL;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public bool InFieldOfView(Vector3 targetPos, float ViewRadius, float Angle)
    {
        Vector3 dir = targetPos - transform.position;// EL PROBLEMA ES QUE TOMA LA POSICION DEL MANAGER, NO LA POSICION DE LOS ENEMIGOS
        if (dir.magnitude > ViewRadius)
        {
            Debug.Log("FALSO 1");
            return false;
        }
        if (!InLineOffSight(transform.position, targetPos))
        {
            Debug.Log("FALSO 2");
            return false;
        }

        return Vector3.Angle(transform.forward, dir) <= Angle / 2;
    }

    public bool InLineOffSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, WALL);
    }
}
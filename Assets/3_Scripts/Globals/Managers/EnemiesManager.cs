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
        //El player se pase a si mismo como referencia o tener DontDetroyOnLoad para ese objeto
        //Sino, se elimina al cambiar de escena. Ser�a mejor la primera opci�n
    }
    public bool InFieldOfView(Enemies EnemyPos, float ViewRadius, float Angle)
    {
        Vector3 dir = _target.transform.position - EnemyPos.transform.position;
        if (dir.magnitude > ViewRadius)
            return false;
        
        if (!InLineOffSight(EnemyPos.transform.position, _target.transform.position))
            return false;

        return Vector3.Angle(EnemyPos.transform.forward, dir) <= Angle / 2;
    }

    public bool InLineOffSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, WALL);
    }
}

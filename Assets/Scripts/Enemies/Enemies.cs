using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Header("Enemy BasicVariables")]
    public int _MaxLife;
    public int _CurrentLife;
    public int _Attack;
    public float _ActualSpeed;
    protected PlayerM targetCollision;
    public PlayerM _target;
    public GameObject _Recompenzas;
    protected GameObject _RecompenzA;

    public virtual void Start()
    {
        _CurrentLife = _MaxLife;
    }

    public virtual void Movement() {}

    public virtual void Destroy()
    {
        _RecompenzA = Instantiate(_Recompenzas, transform.parent);
        _RecompenzA.transform.position = transform.position;
    }
}

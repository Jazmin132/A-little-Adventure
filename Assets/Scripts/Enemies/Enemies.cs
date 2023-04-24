using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    [Header("Enemy BasicVariables")]
    public int _MaxLife;
    public int _CurrentLife;
    public int _Attack;
    public float _ActualSpeed;
    public GameObject _Recompenzas;
    protected PlayerM targetCollision;
    protected PlayerM _Maintarget;
    protected GameObject _RecompenzA;
    protected bool _IsGoing;
    protected Vector3 _FlyTo;

    public virtual void Start()
    {
        _Maintarget = EnemiesManager.instance._target;
        _CurrentLife = _MaxLife;
    }

    public virtual void Movement() {}

    public virtual void Destroy()
    {
        _RecompenzA = Instantiate(_Recompenzas, transform.parent);
        _RecompenzA.transform.position = transform.position;
    }
}

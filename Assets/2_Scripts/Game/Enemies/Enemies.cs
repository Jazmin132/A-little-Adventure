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
        GameManager.instance.onPlay += OnPlay;
        GameManager.instance.onPause += OnPause;
    }

    public virtual void Movement() {}

    public virtual void Destroy()
    {
        _RecompenzA = Instantiate(_Recompenzas, transform.parent);
        _RecompenzA.transform.position = transform.position;
        GameManager.instance.onPause -= OnPause;
        GameManager.instance.onPlay -= OnPlay;
    }

    public virtual void OnPause() {}
    public virtual void OnPlay() {}
}

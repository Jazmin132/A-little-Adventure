using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float _Speed;
    public int _Damage;
    public float _maxDistance;
    protected float _currentDistance;

    protected Rigidbody _Rig;
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    public virtual void Start()
    {
        _Rig = GetComponent<Rigidbody>();
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void Reset()
    {
        _currentDistance = 0;
    }
}

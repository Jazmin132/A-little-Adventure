using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : MonoBehaviour
{
    public int _MaxLife;
    public int _CurrentLife;

    private void Start()
    {
        _CurrentLife = _MaxLife;
    }
    public virtual void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) Destroy();
    }
    public virtual void Destroy()
    {
    }
}

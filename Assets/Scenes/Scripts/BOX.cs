using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX : Damage
{
    public override void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) 
            Destroy(this.gameObject);
    }
}

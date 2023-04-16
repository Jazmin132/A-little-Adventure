using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void RecieveDamage(int damage);
    public void Destroy();
}

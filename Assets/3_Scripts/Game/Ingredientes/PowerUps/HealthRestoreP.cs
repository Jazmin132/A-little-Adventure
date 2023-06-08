using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreP : PowerUp
{
    [SerializeField] int HealthRestore;

    public override void Activate(PlayerM Player)
    {
        Player.life.AddLife(HealthRestore);
    }
}

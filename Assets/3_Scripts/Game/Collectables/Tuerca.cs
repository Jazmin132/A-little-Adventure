using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuerca : Collectables
{
    public override void WhatToAdd()
    {
        CollectablesManager.instance.AddTuerca(value);
    }
}

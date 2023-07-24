using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    public float Vertical();
    public float Horizontal();
    public bool Jump();
    public bool Glide();
    public bool Attack();
    public bool Shoot();
    public bool Acelerate();
    public bool Atajo();
}

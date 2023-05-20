using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : IController
{
    PlayerM _M;

    public Controler(PlayerM M, View V = null)
    {
        _M = M;
        if (V) 
        {
            _M.OnDamage += V.RecieveDamage;
            _M.OnDeath += V.IsDead;
            _M.OnWater += V.Splash;
        }
    }
    public void ListenKeyFixedUpdate() { }
    public void ListenKeyUpdate() { }

    public float Vertical()
    {
        var V = Input.GetAxis("Vertical");
        return V;
    }
    public float Horizontal()
    {
        var H = Input.GetAxis("Horizontal");
        return H;
    }
    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool Glide()
    {
        return Input.GetKey(KeyCode.Mouse1);
    }
    public bool Acelerate()
    {
        return (Input.GetKey(KeyCode.LeftShift));
    }
    public bool Attack()
    {
        return (Input.GetKey(KeyCode.E));
    }
    public bool Shoot()
    {
        return (Input.GetKeyDown(KeyCode.Mouse0));
    }
}

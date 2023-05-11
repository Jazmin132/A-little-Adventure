using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : IController
{
    PlayerM _M;

    public Controler(PlayerM M, View V = null)
    {
        _M = M;
        if (V) { }
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
        return Input.GetKey(KeyCode.Z);
    }
    public void ListenKey()
    {
        var V = Input.GetAxis("Vertical");
        var H = Input.GetAxis("Horizontal");

        _M.MovePlayer(H, V);
        //No permite mas de dos teclas al mismo tiempo, problema de windows
        if (Input.GetKeyDown(KeyCode.Space)) _M.Jump();
        else if (Input.GetKey(KeyCode.Z)) _M.Glide();

        if (Input.GetKey(KeyCode.E))
            _M.Attack();
        else if(Input.GetKeyDown(KeyCode.C))
            _M.Shoot();
        
        if (Input.GetKey(KeyCode.LeftShift))
            _M.Run();
        else if(Input.GetKeyUp(KeyCode.LeftShift)) _M.RunReset();
    }
    public bool Acelerate()
    {
        return (Input.GetKey(KeyCode.LeftShift));
    }//else if(Input.GetKeyUp(KeyCode.LeftShift)) _M.RunReset();
}

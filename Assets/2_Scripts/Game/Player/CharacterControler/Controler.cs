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
            //Pasarle cosas
        }
    }
    public void ListenKeyFixedUpdate()
    {
        var V = Input.GetAxis("Vertical");
        var H = Input.GetAxis("Horizontal");

        _M.MovePlayer(H, V);
    }
    public void ListenKeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _M.Jump();
        else if (Input.GetKey(KeyCode.Z)) _M.Glide();

        if (Input.GetKey(KeyCode.E))
            _M.Attack();
        else if(Input.GetKeyDown(KeyCode.Mouse1))
            _M.Shoot();

        if (Input.GetKey(KeyCode.LeftShift))
            _M.Run();
        else if(Input.GetKeyUp(KeyCode.LeftShift)) _M.RunReset();
    }
}

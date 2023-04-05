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
    public void ListenKey()
    {
        var V = Input.GetAxis("Vertical");
        var H = Input.GetAxis("Horizontal");

        _M.MovePlayer(H, V);

        if (Input.GetKeyDown(KeyCode.Space))
            _M.Jump();
        if (Input.GetKey(KeyCode.Z))
            _M.Glide();
        if (Input.GetKeyDown(KeyCode.X))
            _M.Attack();
        if (Input.GetKey(KeyCode.C))
            _M.Shoot();
        
        if (Input.GetKey(KeyCode.LeftShift))
            _M.Run();
        else if(Input.GetKeyUp(KeyCode.LeftShift)) _M.RunReset();
    }
}

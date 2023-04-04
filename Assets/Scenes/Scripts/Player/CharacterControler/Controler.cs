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

        //_M.MyMovement += _M.MovePlayer;
        _M.MovePlayer(H, V);
        //Lograr hacer que el moviemiento cambien por completo apretando E
        //y se restaure a tocar el piso o soltar E

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    private FiniteStateMachine _FSM;
    IController _Controller;
    PlayerM _Player;
    Rigidbody _RigP;
    float _DescendSpeed;

    public AirState(FiniteStateMachine FSM, PlayerM Player, IController controller)
    {
        _FSM = FSM;
        _Controller = controller;
        _Player = Player;
    }
    public AirState SetRigidbody(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public AirState SetFloat(float DescendSpeed)
    {
        _DescendSpeed = DescendSpeed;
        return this;
    }
    public void OnEnter()
    {
        Debug.Log("ENTER AIR");
    }
    public void OnUpdate()
    {
        if (_Controller.Glide()) Glide();
        Debug.Log("UPDATE AIR");
    }
    public void OnFixedUpdate()
    {
        if (_Player._playerJump.IsGrounded()) _FSM.ChangeState(PlayerStates.Ground);
    }
    public void Glide()
    {//Hacer que avance por sí solo?
        if (_RigP.velocity.y < 0)
            _RigP.velocity = new Vector3(0, -_DescendSpeed, 0);
    }
    public void OnExit()
    {
        Debug.Log("EXIT AIR");
    }
}


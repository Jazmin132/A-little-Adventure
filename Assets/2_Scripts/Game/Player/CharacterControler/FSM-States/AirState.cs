using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    private FiniteStateMachine _FSM;
    Rigidbody _RigP;
    float _DescendSpeed;

    public AirState(FiniteStateMachine FSM)
    {
        _FSM = FSM;
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

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        Glide();
    }
    public void Glide()
    {//Hacer que avance por s� solo?
        if (_RigP.velocity.y < 0)
            _RigP.velocity = new Vector3(0, -_DescendSpeed, 0);
    }
}


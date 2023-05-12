using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    private FiniteStateMachine _FSM;
    IController _Controller;
    PlayerM _Player;
    Rigidbody _RigP;
    Transform _MainCamera;
    Vector3 _direction;
    float _CurrentSpeed;
    float _DescendSpeed;
    float _SpeedH;

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
    public AirState SetFloats(float DescendSpeed, float CurrentSpeed, float SpeedH)
    {
        _DescendSpeed = DescendSpeed;
        _CurrentSpeed = CurrentSpeed;
        _SpeedH = SpeedH;
        return this;
    }
    public AirState SetTransform(Transform MC)
    {
        _MainCamera = MC;
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
    {

        if (_RigP.velocity.y < 0)
        {
            _RigP.velocity = new Vector3(0, -_DescendSpeed, 0);
            //_direction = _Player.transform.forward * _CurrentSpeed;
            _direction = _Player.transform.right * _Controller.Horizontal() * _SpeedH;
            _direction += _Player.transform.forward * _CurrentSpeed;
            
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _Player.transform.rotation = Quaternion.RotateTowards(_Player.transform.rotation, Rotation, Time.fixedDeltaTime * 360f);
            _RigP.MovePosition(_Player.transform.position + _direction * Time.fixedDeltaTime);
        }
    }
    public void OnExit()
    {
        Debug.Log("EXIT AIR");
    }
}


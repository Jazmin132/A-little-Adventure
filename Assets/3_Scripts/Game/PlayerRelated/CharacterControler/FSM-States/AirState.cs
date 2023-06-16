using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    FiniteStateMachine _FSM;
    IController _Controller;
    CapsuleCollider _PlayerCol;
    PlayerM _Player;
    Rigidbody _RigP;
    Transform _MainCamera;
    Vector3 _direction;
    Vector3 _Right;
    Vector3 _Forward;
    float _CurrentSpeed;
    bool _IsDoubleJumping;
    

    public AirState(FiniteStateMachine FSM, PlayerM Player, IController controller)
    {
        _FSM = FSM;
        _Controller = controller;
        _Player = Player;
    }
    public AirState SetRig(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public AirState SetCollider(CapsuleCollider PlayerCol)
    {
        _PlayerCol = PlayerCol;
        return this;
    }
    public AirState SetFloats(float CurrentSpeed)
    {
        _CurrentSpeed = CurrentSpeed;
        return this;
    }
    public AirState SetTransform(Transform MC)
    {
        _MainCamera = MC;
        return this;
    }

    public void OnEnter()
    {
        _PlayerCol.material = _Player.PhysicsM[1];
        _IsDoubleJumping = false;
        Debug.Log("ENTER AIR");
    }
    public void OnUpdate()
    {
        if (_Controller.Jump() && _Player.jump.IsDJumpActive && _IsDoubleJumping == false)
        {
            _Player._playerJump.Doublejump(10);
            Debug.Log("Using DoubleJump");
            _IsDoubleJumping = true;
        }
    }
    public void OnFixedUpdate()
    {
        if (_Controller.Glide()) _FSM.ChangeState(PlayerStates.Glide);
        else MoveOnAir();

        if (_Controller.Shoot()) _Player.Shoot();
        else if (_Controller.Attack()) _Player.Attack();

        if (_Player._playerJump.IsGrounded() && _RigP.velocity.y < 0) _FSM.ChangeState(PlayerStates.Ground);

        if (_Controller.Atajo()) _Player.ActivateCheckPoint();
    }
   
    public void MoveOnAir()
    {
        _Forward = Vector3.ProjectOnPlane(_MainCamera.transform.forward, Vector3.up).normalized;
        _Right = Vector3.ProjectOnPlane(_MainCamera.transform.right, Vector3.up).normalized;
        _direction = (_Controller.Horizontal() * _Right + _Controller.Vertical() * _Forward);

        if (_direction.sqrMagnitude > 1) _direction.Normalize();

        if (_Player.WallDetecter(_direction)) return;

        if (_Controller.Horizontal() != 0 || _Controller.Vertical() != 0)
        {
            _RigP.MovePosition(_Player.transform.position + _direction * _CurrentSpeed * Time.fixedDeltaTime);
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _Player.transform.rotation = Quaternion.RotateTowards(_Player.transform.rotation, Rotation, Time.fixedDeltaTime * 500);
        }
    }
    public void OnExit()
    {
       
    }
}


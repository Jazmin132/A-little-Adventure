using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideState : IState
{
    FiniteStateMachine _FSM;
    IController _Controller;
    Transform _MainCamera;
    Rigidbody _RigP;
    PlayerM _Player;
    Vector3 _direction;
    float _GlidingSign;
    float _CurrentSpeed;
    float _DescendSpeed;
    float _SpeedH;
    bool _IsDoubleJumping;

    public GlideState(FiniteStateMachine FSM, PlayerM Player, IController controller)
    {
        _FSM = FSM;
        _Controller = controller;
        _Player = Player;
    }
    public GlideState SetRig(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public GlideState SetFloats(float DescendSpeed, float CurrentSpeed, float SpeedH)
    {
        _DescendSpeed = DescendSpeed;
        _CurrentSpeed = CurrentSpeed;
        _SpeedH = SpeedH;
        return this;
    }

    public GlideState SetTransform(Transform MC)
    {
        _MainCamera = MC;
        return this;
    }

    public void OnEnter()
    {
        Debug.Log("ENTER GLIDE");
        _GlidingSign = Mathf.Sign(Vector3.Dot(_Player.transform.forward, _MainCamera.transform.forward));
        _IsDoubleJumping = false;
    }

    public void OnUpdate()
    {
        if (_Controller.Jump() && _Player.jump.IsDJumpActive && _IsDoubleJumping == false)
        {
            _RigP.AddForce(Vector3.up * 2, ForceMode.VelocityChange);
            _IsDoubleJumping = true;
        }
    }

    public void OnFixedUpdate()
    {
        Glide();
        if (_Controller.Shoot()) _Player.Shoot();
        else if (_Controller.Attack()) _Player.Attack();

        if (!_Controller.Glide()) _FSM.ChangeState(PlayerStates.Air);
        if (_Player._playerJump.IsGrounded() && _RigP.velocity.y < 0) _FSM.ChangeState(PlayerStates.Ground);
        if (_Controller.Atajo()) _Player.ActivateCheckPoint();
    }

    public void Glide()
    {
        if (_RigP.velocity.y < 0)
        {
            _direction = (_Player.transform.right * _GlidingSign) * _Controller.Horizontal() * _SpeedH;
            _direction += _Player.transform.forward * (_CurrentSpeed + 3);

            _RigP.velocity = new Vector3(0, -_DescendSpeed, 0);
            if (_Player.WallDetecter(_direction)) return;

            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _Player.transform.rotation = Quaternion.RotateTowards(_Player.transform.rotation, Rotation, Time.fixedDeltaTime * 500);
            _RigP.MovePosition(_Player.transform.position + _direction * Time.fixedDeltaTime);
        }
    }

    public void OnExit()
    {

    }
}


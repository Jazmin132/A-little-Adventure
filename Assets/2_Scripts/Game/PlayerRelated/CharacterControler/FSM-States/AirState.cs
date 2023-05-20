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
    float _DescendSpeed;
    float _SpeedH;
    float _GlidingSign;
    bool _IsGliding;

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
    {//HAY UN RARO DELAY AL ENTRAR A ESTE ESTADO
        Debug.Log("ENTER AIR");
        _PlayerCol.material = _Player.PhysicsM[1];
    }
    public void OnUpdate()
    {
        
    }
    public void OnFixedUpdate()
    {
        if (_Controller.Glide())
        {
            _GlidingSign = Mathf.Sign(Vector3.Dot(_Player.transform.forward, _MainCamera.transform.forward));

            Glide();
        }
        else MoveOnAir();

        if (_Controller.Shoot()) _Player.Shoot();
        else if (_Controller.Attack()) _Player.Attack();

        if (_Player._playerJump.IsGrounded()) _FSM.ChangeState(PlayerStates.Ground);
    }
    public void Glide()
    {//HACER QUE LA DERECHA SIEMPRE SEA LA DERECHA DE LA CAMARA
     //PERO QUE NO DEJE DE GIRAR´A MENOS QUE SUELTE LA TECLA
        if (_RigP.velocity.y < 0)
        {//Devuelve +1 o -1

            _direction = (_Player.transform.right * _GlidingSign) * _Controller.Horizontal() * _SpeedH;
            _direction += _Player.transform.forward * _CurrentSpeed;
            
            _RigP.velocity = new Vector3(0, -_DescendSpeed, 0);
            if (_Player.WallDetecter(_direction)) return;

            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _Player.transform.rotation = Quaternion.RotateTowards(_Player.transform.rotation, Rotation, Time.fixedDeltaTime * 500);
            _RigP.MovePosition(_Player.transform.position + _direction * Time.fixedDeltaTime);
        }
    }
    public void MoveOnAir()
    {
        _Forward = Vector3.ProjectOnPlane(_MainCamera.transform.forward, Vector3.up).normalized;
        _Right = Vector3.ProjectOnPlane(_MainCamera.transform.right, Vector3.up).normalized;
        _direction = (_Controller.Horizontal() * _Right + _Controller.Vertical() * _Forward).normalized;

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
        Debug.Log("EXIT AIR");
        _IsGliding = false;
    }
}


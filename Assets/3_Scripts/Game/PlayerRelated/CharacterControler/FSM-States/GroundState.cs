using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : IState
{
    FiniteStateMachine _FSM;
    IController _Controller;
    PlayerM _Player;
    CapsuleCollider _PlayerCol;
    Rigidbody _RigP;
    Transform _transform;
    Transform _MainCamera;
    float _CurrentSpeed;
    float _OriginalSpeed;
    float _RayDownDist;
    Vector3 _direction;
    LayerMask _Water;

    public GroundState(FiniteStateMachine FSM, PlayerM Player, IController controller)
    {
        _FSM = FSM;
        _Player = Player;
        _Controller = controller;
    }
    public GroundState SetRig(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public GroundState SetCollider(CapsuleCollider PlayerCol)
    {
        _PlayerCol = PlayerCol;
        return this;
    }
    public GroundState SetTransforms(Transform T, Transform MainCamera)
    {
        _transform = T;
        _MainCamera = MainCamera;
        return this;
    }
    public GroundState SetSpeed(float Speed, float PlayerSpeed, float RayDownDist)
    {
        _CurrentSpeed = Speed;
        _OriginalSpeed = PlayerSpeed;
        _RayDownDist = RayDownDist;
        return this;
    }
    public GroundState SetLayers(LayerMask Water)
    {
        _Water = Water;
        return this;
    }

    public void OnEnter()
    {
        Debug.Log("ENTER GROUND");
        _CurrentSpeed = _OriginalSpeed;
        _PlayerCol.material = _Player.PhysicsM[0];
        _Player.CheckEnviroment();
    }

    public void OnUpdate()
    {
        if (_Controller.Jump()) _Player._playerJump.Jump();
    }

    public void OnFixedUpdate()
    {
        Move();
        //PREGUNTAR COMO OPTIMIZAR ESTO
        if (_Controller.Shoot()) _Player.Shoot();
        else if (_Controller.Attack()) _Player.Attack();

        if (!_Player._playerJump.IsGrounded()) _FSM.ChangeState(PlayerStates.Air);

        if (_Controller.Atajo()) _Player.ActivateCheckPoint();
    }
    private void Move()
    {
        //Vector3.ProjectOnPlane proyecta vector sobre una superficie plana/ Vector3.up = plano Z
        Vector3 Forward = Vector3.ProjectOnPlane(_MainCamera.transform.forward, Vector3.up).normalized;
        Vector3 Right = Vector3.ProjectOnPlane(_MainCamera.transform.right, Vector3.up).normalized;
        _direction = (_Controller.Horizontal() * Right + _Controller.Vertical() * Forward);

        if (_direction.sqrMagnitude > 1) _direction.Normalize();

        if (_Player.WallDetecter(_direction)) return;

        if (_Controller.Horizontal() != 0 || _Controller.Vertical() != 0)
        {
            _RigP.MovePosition(_transform.position + _direction * _CurrentSpeed * Time.fixedDeltaTime);
            //Que gire sobre su vector Y hacia la dirreción que le indico
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Rotation, Time.fixedDeltaTime * 500f);
            //Que rote, a partir de su rotación actual hacia la que le indico, con una radio específico, multiplico po 4 para acelerar
        }
    }

    public void OnExit()
    {
        //Debug.Log("EXIT GROUND");
    }
}

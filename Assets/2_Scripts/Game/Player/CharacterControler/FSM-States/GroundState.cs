using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : IState
{
    private FiniteStateMachine _FSM;
    private PlayerM _Player;
    private IController _Controller;
    Rigidbody _RigP;
    Transform _transform;
    Transform _MainCamera;
    float _CurrentSpeed;
    Vector3 _direction;

    public GroundState(FiniteStateMachine FSM, PlayerM Player, IController controller)
    {
        _FSM = FSM;
        _Player = Player;
        _Controller = controller;
    }
    public GroundState SetRigidbody(Rigidbody Rig)
    {
        _RigP = Rig;
        return this;
    }
    public GroundState SetTransforms(Transform T, Transform MainCamera)
    {
        _transform = T;
        _MainCamera = MainCamera;
        return this;
    }
    public GroundState SetSpeed(float Speed)
    {
        _CurrentSpeed = Speed;
        return this;
    }
    public void OnEnter()
    {
        Debug.Log("ENTER GROUND");
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {
        Debug.Log("FixedUpdate Ground");
        //Vector3.ProjectOnPlane proyecta vector sobre una superficie plana/ Vector3.up = plano Z

        Vector3 Forward = Vector3.ProjectOnPlane(_MainCamera.transform.forward, Vector3.up).normalized;
        Vector3 Right = Vector3.ProjectOnPlane(_MainCamera.transform.right, Vector3.up).normalized;
        _direction = (_Controller.Horizontal() * Right + _Controller.Vertical() * Forward).normalized;

        if (_Controller.Horizontal() != 0 || _Controller.Vertical() != 0)
        {
            _RigP.MovePosition(_transform.position + _direction * _CurrentSpeed * Time.fixedDeltaTime);
            //Que gire sobre su vector Y hacia la dirreci�n que le indico
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Rotation, Time.fixedDeltaTime * 360f * 4);
            //Que rote, a partir de su rotaci�n actual hacia la que le indico, con una radio espec�fico, multiplico po 4 para alentizar
        }

        Jump();

        if (!_Player._playerJump.IsGrounded()) _FSM.ChangeState(PlayerStates.Air);
    }
    public void Jump()
    {
        if (_Controller.Jump())
        {
            _Player._playerJump.Jump();
            _FSM.ChangeState(PlayerStates.Air);
        }
    }
   /* public void Shoot()
    {
        RaycastHit hit; //Como hago para que el instantiate funcione?
        _bulletObject = Instantiate(_BulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = _bulletObject.GetComponent<Bullet>();
        if (Physics.Raycast(_MainCamera.position, _MainCamera.forward, out hit, Mathf.Infinity))
        {
            bullet.target = hit.point;
            bullet.hit = true;
        }
        else
        {
            bullet.target = _MainCamera.position + _MainCamera.forward * _MaxDistAir;
            bullet.hit = true;
        }
    }*/
    public void OnExit()
    {
        Debug.Log("EXIT GROUND");
    }
}

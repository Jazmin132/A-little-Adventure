using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : IState
{
    private FiniteStateMachine _FSM;
    private PlayerM _Player;
    Rigidbody _RigP;
    Transform _transform;
    Transform _MainCamera;
    float _CurrentSpeed;
    Vector3 _direction;

    public GroundState(FiniteStateMachine FSM, PlayerM Player)
    {
        _FSM = FSM;
        _Player = Player;
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
        Debug.Log("ENTER");
        _Player.Suscribe(MovePlayer);
    }

    public void OnUpdate()
    {
        Debug.Log("UPDATE");
    }

    public void MovePlayer(float H, float V)
    {
        _FSM.FakeUpdate();
        //Vector3.ProjectOnPlane proyecta vector sobre una superficie plana/ Vector3.up = plano Z
        Vector3 Forward = Vector3.ProjectOnPlane(_MainCamera.transform.forward, Vector3.up).normalized;
        Vector3 Right = Vector3.ProjectOnPlane(_MainCamera.transform.right, Vector3.up).normalized;
        _direction = (H * Right + V * Forward).normalized;

        //if (_playerJump.IsGrounded()) IsJumping = false;
        //else IsJumping = true;

        //TENER FSM PARA CAMBIAR EL MOVIMIENTO
        if (H != 0 || V != 0)
        {//Agregar una miniaceleración
            _RigP.MovePosition(_transform.position + _direction * _CurrentSpeed * Time.fixedDeltaTime);
            //Que gire sobre su vector Y hacia la dirreción que le indico
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Rotation, Time.fixedDeltaTime * 360f * 4);
            //Que rote, a partir de su rotación actual hacia la que le indico, con una radio específico, multiplico po 4 para alentizar
        }
    }

    public void OnExit()
    {
        Debug.Log("EXIT");
        _Player.UnSuscribe(MovePlayer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerM : MonoBehaviour
{
    [Header("Life and Damage")]
    [SerializeField] int _MaxLife;
    [SerializeField] int _CurrentLife;
    [SerializeField] int _Damage;
    [SerializeField] float _AttackDuration;
    [SerializeField] float _AttackReload;

    [Header("Normal Movement")]
    [SerializeField] float _Speed;
    private float _CurrentSpeed;
    private BoxCollider _AttackBox;
    private Rigidbody _RigP;
    private Camera _playerCamera;
    private Vector3 _direction;
    //Intentar arreglarlo?
    private bool _CanRun = false;

    [Header("Jump")]
    [SerializeField] bool IsJumping = false;
    [SerializeField] float _RayJumpDist;
    [SerializeField] float _JumpForce;

    [SerializeField] float _FallMultiplier;
    [SerializeField] float VelocityFalloff;

    [Header("Glide")]
    [SerializeField] float _GlideDescendSpeed;

    [Header("Shoot")]
    [SerializeField] GameObject _BulletPrefab;
    [SerializeField] float _MaxRayDist;
    private Ray _CenterRay;
    Vector3 _DirCamara = new Vector3(Screen.width/2f, Screen.height / 2f, 0f);

    IController _controller;
    PlayerJump _playerJump;
    private HearthDisplay _lifeManager;

    private void Awake()
    {
        _RigP = GetComponent<Rigidbody>();
        _controller = new Controler(this, GetComponent<View>());
        _playerJump = new PlayerJump().SetJump(_RayJumpDist, _JumpForce).SetRigidbody(_RigP);
    }
    private void Start()
    {
        _CurrentSpeed = _Speed;
        _CurrentLife = _MaxLife;
        _playerCamera = Camera.main;
        _AttackBox = GetComponent<BoxCollider>();
        _lifeManager = FindObjectOfType<HearthDisplay>();
    }
    void FixedUpdate()
    {
        _controller.ListenKey();
    }
    public void MovePlayer(float H, float V)
    {//Vector3.ProjectOnPlane proyecta vector sobre una superficie plana/ Vector3.up = plano Z
        Vector3 Forward = Vector3.ProjectOnPlane(_playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 Right = Vector3.ProjectOnPlane(_playerCamera.transform.right, Vector3.up).normalized;
        _direction = (H * Right + V * Forward).normalized;

        GravityModifier();
        if(_playerJump.IsGrounded()) IsJumping = false;

        //Verificar si est� grounded una sola vez, no todo el tiempo
        if (H != 0 || V != 0 && _direction.magnitude >= 0.01f)
        {//Agregar una miniaceleraci�n
            _RigP.position += _direction * _CurrentSpeed * Time.fixedDeltaTime;
            //Que gire sobre su vector Y hacia la dirreci�n que le indico
            Quaternion Rotation = Quaternion.LookRotation(_direction.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, Time.fixedDeltaTime * 360f * 4);
            //Que rote, a partir de su rotaci�n actual hacia la que le indico, con una radio espec�fico, multiplico po 4 para alentizar
        }
    }
    public void Jump()
    {
        if (IsJumping == false)
        {
            _playerJump.Jump();
            IsJumping = true;
            Debug.Log("Jumping");
        }//No salta todo el tiempo, a veces se traba, sobretodo si corro o voy a la izquierda/arriba
    }//Lograr que el glide se active despu�s del Jump normal, manteniendo la tecla apretada
    public void Run()
    {
        if (_playerJump.IsGrounded())
        {
            _CurrentSpeed = Math.Clamp(_CurrentSpeed, 0, _Speed * 1.5f);
            _CurrentSpeed = _Speed * 1.5f;
        }
    }
    public void RunReset()
    {
        _CurrentSpeed = _Speed;
    }
    public void Glide()
    {//Hacer que avance por s� solo
        if (_RigP.velocity.y < 0)
            _RigP.velocity = new Vector3(0, -_GlideDescendSpeed, 0);
    }
    public void Attack()
    {
        _AttackBox.enabled = true;
        StartCoroutine(Recharge(_AttackDuration, _AttackReload));
    }
    IEnumerator Recharge(float AttackD, float ReloadT)
    {
        var Wait = new WaitForSeconds(AttackD);
        yield return Wait;
        _AttackBox.enabled = false;
        Debug.Log("Cant attack");
        yield return Wait;
        Debug.Log("Can Attack again");
    }
    public void Aim()
    {
        _CenterRay = _playerCamera.ViewportPointToRay(_DirCamara);
        Debug.DrawLine(_CenterRay.origin, _CenterRay.origin + _CenterRay.direction * _MaxRayDist, Color.red);
        
        if (Physics.Raycast(_CenterRay, out RaycastHit hit, _MaxRayDist))
        {
            Debug.Log("name: " + hit.collider.name);
        }
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        var bullet = Instantiate(_BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = _CenterRay.direction;
    }

    public void GravityModifier()
    {
        if (_RigP.velocity.y < VelocityFalloff)
            _RigP.velocity += Vector3.up * Physics.gravity.y * (_FallMultiplier - 1) * Time.fixedDeltaTime;
    }

    public void RecieveHit(int damage)
    {
        _CurrentLife -= damage;
        Debug.Log("AUCH " + _CurrentLife);
        if (_CurrentLife <= 0)
        {
            _CurrentLife = 0;
            Debug.Log("Moriste");
        }
        _lifeManager.UpdateHealth(_CurrentLife);
    }
    private void OnTriggerEnter(Collider other)
    {
        var D = other.GetComponent<IDamage>();
        if (D != null) D.RecieveDamage(_Damage);
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 X = new Vector3(0f, -_RayJumpDist, 0f);
        Gizmos.DrawLine(transform.position, transform.position + X);
    }
}

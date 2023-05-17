using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerStates
{
    Ground,
    Air,
    Water
}

public class PlayerM : MonoBehaviour
{
    [Header("Life and Attack")]
    [SerializeField] int _MaxLife;
    [SerializeField] int _CurrentLife;
    [SerializeField] int _Damage;
    [SerializeField] float _AttackDuration;
    [SerializeField] float _AttackReload;
    private bool _OnAttack = false;

    [Header("Normal Movement")]
    [SerializeField] float _PlayerSpeed;
    [SerializeField] float _RayForwardDist;
    [SerializeField] float _RayUpDist;
    [SerializeField] float _RayDownDist;
    public PhysicMaterial[] PhysicsM;
    private Vector3 _UpDist;
    private Vector3 _DownDist;
    private float _CurrentSpeed;
    private BoxCollider _AttackBox;
    private CapsuleCollider _PlayerCol;
    private Rigidbody _RigP;
    private Transform _MainCamera;
    private Vector3 _direction;
    bool Ray = false;

    [Header("Jump")]
    [SerializeField] float _JumpForce;
    [SerializeField] float _RayJumpDist;
    [SerializeField] bool IsJumping;

    [SerializeField] float _FallMultiplier;
    [SerializeField] float VelocityFalloff;

    [Header("Glide")]
    [SerializeField] float _DescendSpeed;
    [SerializeField] float _SpeedHorizontal;

    [Header("Shoot")]
    [SerializeField] GameObject _BulletPrefab;
    [SerializeField] GameObject _BulletParent;
    private GameObject _bulletObject;
    [SerializeField] Transform _firePoint;

    [SerializeField] float _MaxDistAir;

    IController _controller;
    public PlayerJump _playerJump;
    private HearthDisplay _lifeManager;
    private FiniteStateMachine _FSM;
    Vector3 CheckPointPosition;
    Vector3 CheckPointRotation;

    public event Action OnDamage;
    public event Action OnDeath;

    private void Awake()
    {
        _RigP = GetComponent<Rigidbody>();
        _controller = new Controler(this, GetComponent<View>());
        _playerJump = new PlayerJump().SetJump(_RayJumpDist, _JumpForce).SetRigidbody(_RigP);

        _AttackBox = GetComponent<BoxCollider>();
        _PlayerCol = GetComponent<CapsuleCollider>();
        _lifeManager = FindObjectOfType<HearthDisplay>();
    }
    private void Start()
    {
        _CurrentSpeed = _PlayerSpeed;
        _CurrentLife = _MaxLife;
        _MainCamera = Camera.main.transform;

        _UpDist = new Vector3(0f, _RayUpDist, 0f);
        _DownDist = new Vector3(0f, _RayDownDist, 0f);

        _FSM = new FiniteStateMachine();
        var groundState = new GroundState(_FSM, this, _controller).SetCollider(_PlayerCol)
            .SetTransforms(transform, _MainCamera).SetRig(_RigP).SetSpeed(_CurrentSpeed, _PlayerSpeed);

        var AirState = new AirState(_FSM, this, _controller).SetRig(_RigP).SetCollider(_PlayerCol)
            .SetTransform(_MainCamera).SetFloats(_DescendSpeed, _CurrentSpeed, _SpeedHorizontal);

        _FSM.AddState(PlayerStates.Ground, groundState);
        _FSM.AddState(PlayerStates.Air, AirState);
        _FSM.ChangeState(PlayerStates.Ground);
    }

    private void Update()
    {
        _FSM.FakeUpdate();
    }
    void FixedUpdate()
    {
        _FSM.FakeFixedUpdate();
        GravityModifier();
    }
   
    public void UpImpulse(float ForceUp)
    {
        _RigP.AddForce(Vector3.up * ForceUp, ForceMode.VelocityChange);
    }

    //SE QUEDA ACÁ
    public void Attack()
    {
        _AttackBox.enabled = true;
        _OnAttack = true;
        StartCoroutine(Recharge(_AttackDuration, _AttackReload));
    }
    IEnumerator Recharge(float AttackD, float ReloadT)
    {
        var Wait = new WaitForSeconds(AttackD);
        yield return Wait;
        _AttackBox.enabled = false;
        _OnAttack = false;
        Debug.Log("Cant attack");
        yield return Wait;
        Debug.Log("Can Attack again");
    }

    public bool WallDetecter(Vector3 dir)
    {
        var Down = Physics.Raycast(_RigP.transform.position + _UpDist, dir, _RayForwardDist);
        var Up = Physics.Raycast(_RigP.transform.position - _DownDist, dir, _RayForwardDist);

        if (Down && Up) Ray = true;
        else Ray = false;

        return Ray;
    }
    public void Shoot()
    {
        RaycastHit hit;
        _bulletObject = Instantiate(_BulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = _bulletObject.GetComponent<Bullet>();
        if (Physics.Raycast(_MainCamera.position, _MainCamera.forward, out hit, Mathf.Infinity))
        {
            _bulletObject.transform.forward = _MainCamera.forward;
            bullet.target = hit.point;
            bullet.hit = true;
        }
        else
        {
            _bulletObject.transform.forward = _MainCamera.forward;
            bullet.target = _MainCamera.position + _MainCamera.forward * _MaxDistAir;
            bullet.hit = true;
        }
        Vector3 camForward = _MainCamera.forward;
        camForward.y = 0;
        transform.forward = camForward;
    }
    public void RecieveHit(int damage)
    {
        _CurrentLife -= damage;
        OnDamage?.Invoke();
        if (_CurrentLife <= 0)
        {
            _CurrentLife = 0;
            OnDeath?.Invoke();
        }
        Debug.Log("AUCH " + _CurrentLife);
        _lifeManager.UpdateHealth(_CurrentLife);
    }
    public void NewCheckPoint()
    {
        CheckPointPosition = transform.position;
        CheckPointRotation = transform.position;
    }
    public void GravityModifier()
    {
        if (_RigP.velocity.y < VelocityFalloff)
            _RigP.velocity += Vector3.up * Physics.gravity.y * (_FallMultiplier - 1) * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var D = other.GetComponent<IDamage>();
        var I = other.GetComponent<Ingredient>();

        if (D != null) D.RecieveDamage(_Damage);
        else if (I != null && _OnAttack && I.CanBeHit)
        {
            I.Activate();
            Debug.Log("Nice");
        }
    }
    public void OnDrawGizmos()
    {
        Vector3 X = new Vector3(0f, -_RayJumpDist, 0f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + X);
       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + _UpDist, transform.position + _UpDist + _direction * _RayForwardDist);
        Gizmos.DrawLine(transform.position - _DownDist, transform.position - _DownDist + _direction * _RayForwardDist);
        Gizmos.DrawLine(transform.position - _DownDist + (_direction * _RayForwardDist), transform.position + _UpDist + (_direction * _RayForwardDist));
    }
}

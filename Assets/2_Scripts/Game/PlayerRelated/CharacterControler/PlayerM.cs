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

public class PlayerM : MonoBehaviour, IGetHealth
{
    public Health life;

    [Header("Attack")]
    //COMO HAGO PARA PONER EN OTRA CLASE A ATTACK Y QUE FUNCIONE EL IENUMERATOR
    [SerializeField] int _Damage;
    [SerializeField] float _AttackDuration;
    [SerializeField] float _AttackReload;
    private BoxCollider _AttackBox;
    private bool _OnAttack = false;

    //COMO HAGO en este caso, Rigidbody debería seguir aquí?
    [Header("Normal Movement")]
    [SerializeField] private float _PlayerSpeed;
    [SerializeField] float _RayForwardDist;
    [SerializeField] float _RayUpDist;
    [SerializeField] float _RayDownDist;
    [SerializeField] LayerMask _Wall;
    [SerializeField] LayerMask _Water;
    public PhysicMaterial[] PhysicsM;
    private Vector3 _UpDist;
    private Vector3 _DownDist;
    private float _CurrentSpeed;
    private CapsuleCollider _PlayerCol;
    private Rigidbody _RigP;
    private Transform _MainCamera;
    float _RayCheckDist;
   
    bool Ray = false;

    public Jump jump;

    public Glide glide;

    [Header("Shoot")]
    [SerializeField] GameObject _BulletPrefab;
    [SerializeField] Transform _firePoint;

    [SerializeField] float _MaxDistAir;

    IController _controller;
    public PlayerJump _playerJump;
    private FiniteStateMachine _FSM;
    Vector3 CheckPointPosition;
    Vector3 _direction;
    Quaternion CheckPointRotation;
    Behaviour script;

    public event Action OnWater;

    private void Awake()
    {
        _RigP = GetComponent<Rigidbody>();
        script = GetComponent<Behaviour>();
        _controller = new Controler(this, GetComponent<View>());
        _playerJump = new PlayerJump().SetJump(jump.RayJumpDist, jump.JumpForce).SetRigidbody(_RigP);

        _AttackBox = GetComponent<BoxCollider>();
        _PlayerCol = GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        _CurrentSpeed = _PlayerSpeed;
        _RayCheckDist = jump.RayJumpDist;
        life._CurrentLife = life._MaxLife;
        _MainCamera = Camera.main.transform;
        CheckPoint();
        _UpDist = new Vector3(0f, _RayUpDist, 0f);
        _DownDist = new Vector3(0f, _RayDownDist, 0f);

        _FSM = new FiniteStateMachine();
        var groundState = new GroundState(_FSM, this, _controller).SetCollider(_PlayerCol)
            .SetTransforms(transform, _MainCamera).SetRig(_RigP).SetLayers(_Water)
            .SetSpeed(_CurrentSpeed, _PlayerSpeed, _RayDownDist);

        var AirState = new AirState(_FSM, this, _controller).SetRig(_RigP).SetCollider(_PlayerCol)
            .SetTransform(_MainCamera).SetFloats(glide.DescendSpeed, _CurrentSpeed, glide.SpeedHorizontal);

        _FSM.AddState(PlayerStates.Ground, groundState);
        _FSM.AddState(PlayerStates.Air, AirState);
        _FSM.ChangeState(PlayerStates.Ground);

        ScenesManager.instance.onCheckPoint += ActivateCheckPoint;
        //GameManager.instance.onPause += Disable;
        //GameManager.instance.onPlay += Enable; No funciona ;(
    }

    void Update()
    {
        _FSM.FakeUpdate();
    }
    void FixedUpdate()
    {
        _FSM.FakeFixedUpdate();
        GravityModifier();
    }
    //SE QUEDA ACÁ

    public bool WallDetecter(Vector3 dir)
    {
        var Down = Physics.Raycast(_RigP.transform.position + _UpDist, dir, _RayForwardDist, _Wall);
        var Up = Physics.Raycast(_RigP.transform.position - _DownDist, dir, _RayForwardDist, _Wall);
        //HACER QUE SUBA LA ESCALERA, POR AHORA SE QUEDA ASÍ
        if (Down && Up) Ray = true;
        else Ray = false;

        return Ray;
    }
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
    public void UpImpulse(float ForceUp)
    {
        _RigP.AddForce(Vector3.up * ForceUp, ForceMode.VelocityChange);
    }
    public void Shoot()
    {
        RaycastHit hit;
        var _bulletObject = BulletFactory._instance.pool.GetObject();
        _bulletObject.transform.position = _firePoint.position;
    
        if (Physics.Raycast(_MainCamera.position, _MainCamera.forward, out hit, Mathf.Infinity))
        {
            
            _bulletObject.transform.forward = _MainCamera.forward;
            _bulletObject.target = hit.point;
            _bulletObject.hit = true;
        }
        else
        {
            _bulletObject.transform.forward = _MainCamera.forward;
            _bulletObject.target = _MainCamera.position + _MainCamera.forward * _MaxDistAir;
            _bulletObject.hit = true;
        }
        Vector3 camForward = _MainCamera.forward;
        camForward.y = 0;
        transform.forward = camForward;
    }

    public void CheckEnviroment()
    {
        if (Physics.Raycast(_RigP.transform.position, Vector3.down, jump.RayJumpDist, _Water))
        {
            Debug.Log("AGUA");
            OnWater?.Invoke();
        }
    }
    public void CheckPoint()
    {
        CheckPointPosition = transform.position;
        CheckPointRotation = transform.rotation;
    }
    public void ActivateCheckPoint()
    {
        transform.position = CheckPointPosition;
        transform.rotation = CheckPointRotation;
    }

    private void GravityModifier()
    {
        if (_RigP.velocity.y < jump.VelocityFalloff)
            _RigP.velocity += Vector3.up * Physics.gravity.y * (jump.FallMultiplier - 1) * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var I = other.GetComponent<Ingredient>();
        if (other.TryGetComponent(out IDamage D)) D.RecieveDamage(_Damage);
        else if (I != null && _OnAttack && I.CanBeHit) I.Activate();
    }
    private void OnDrawGizmos()
    {
        Vector3 X = new Vector3(0f, -jump.RayJumpDist, 0f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + X);
       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + _UpDist, transform.position + _UpDist + _direction * _RayForwardDist);
        Gizmos.DrawLine(transform.position - _DownDist, transform.position - _DownDist + _direction * _RayForwardDist);
        Gizmos.DrawLine(transform.position - _DownDist + (_direction * _RayForwardDist), transform.position + _UpDist + (_direction * _RayForwardDist));
    }

    public void Disable()
    {
        script.enabled = false;
    }

    public void Enable()
    {
        script.enabled = true;
    }

    public Health GetHealth()
    {
        return life;
    }
}


[System.Serializable]
public class Health
{
    public event Action<float> OnDamage;
    public event Action OnDeath;

    public int _MaxLife;
    public int _CurrentLife;

    public void RecieveHit(int damage)
    {
        _CurrentLife -= damage;

        OnDamage?.Invoke(_CurrentLife);

        if (_CurrentLife <= 0)
        {
            _CurrentLife = 0;
            OnDeath?.Invoke();
        }
        Debug.Log("AUCH " + _CurrentLife);
    }

    public void AddLife(int restore)
    {
        _CurrentLife += restore;
        if (_CurrentLife >= _MaxLife)
            _CurrentLife = _MaxLife;
    }

    public void ResetLife()
    {
        _CurrentLife = _MaxLife;
    }
}
public interface IGetHealth
{
    Health GetHealth();
}

[System.Serializable]
public class Glide
{
    public float DescendSpeed;
    public float SpeedHorizontal;
}

[System.Serializable]
public class Jump
{
    public float JumpForce;//11.3
    public float RayJumpDist;//0.87
    public bool IsJumping;

    public float FallMultiplier;//4.5
    public float VelocityFalloff;//8
}
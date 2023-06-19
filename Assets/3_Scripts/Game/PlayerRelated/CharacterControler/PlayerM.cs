using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerStates
{
    Ground,
    Air,
    Glide
}

public class PlayerM : MonoBehaviour, IDamageableBomb
{
    public PlayerHealth life;

    [Header("Attack")]
    //COMO HAGO PARA PONER EN OTRA CLASE A ATTACK Y QUE FUNCIONE EL IENUMERATOR
    [SerializeField] int _Damage;
    [SerializeField] float _AttackDuration;
    [SerializeField] float _AttackReload;
    public bool IsPowerAttack = false;
    BoxCollider _AttackBox;
    bool _IsAttacking = false;

    //COMO HAGO en este caso, Rigidbody debería seguir aquí?
    [Header("Normal Movement")]
    [SerializeField] private float _PlayerSpeed;
    [SerializeField] float _RayForwardDist;
    [SerializeField] float _RayUpDist;
    [SerializeField] float _RayDownDist;
    [SerializeField] LayerMask _Wall;
    [SerializeField] LayerMask _Water;
    public PhysicMaterial[] PhysicsM;
    Vector3 _UpDist;
    Vector3 _DownDist;
    float _CurrentSpeed;
    CapsuleCollider _PlayerCol;
    Rigidbody _RigP;

    Transform _MainCamera;
    float _RayCheckDist;
   
    bool Ray = false;

    public PJump jump;

    public Glide glide;

    [Header("Shoot")]
    [SerializeField] Transform _firePoint;

    [SerializeField] float _MaxDistAir;

    IController _controller;
    public PlayerJump _playerJump;
    private FiniteStateMachine _FSM;
    Vector3 CheckPointPosition;
    Vector3 _direction;
    Quaternion CheckPointRotation;

    public event Action OnWater;
    public event Action<float, int> OnAttack;

    private void Awake()
    {
        _RigP = GetComponent<Rigidbody>();
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
        var GroundState = new GroundState(_FSM, this, _controller).SetCollider(_PlayerCol)
            .SetTransforms(transform, _MainCamera).SetRig(_RigP).SetLayers(_Water)
            .SetSpeed(_CurrentSpeed, _PlayerSpeed, _RayDownDist);

        var AirState = new AirState(_FSM, this, _controller).SetRig(_RigP).SetCollider(_PlayerCol)
            .SetTransform(_MainCamera).SetFloats(_CurrentSpeed);

        var GlideState = new GlideState(_FSM, this, _controller).SetRig(_RigP).SetTransform(_MainCamera)
            .SetFloats(glide.DescendSpeed, _CurrentSpeed, glide.SpeedHorizontal);

        _FSM.AddState(PlayerStates.Ground, GroundState);
        _FSM.AddState(PlayerStates.Air, AirState);
        _FSM.AddState(PlayerStates.Glide, GlideState);
        _FSM.ChangeState(PlayerStates.Ground);

        ScenesManager.instance.onCheckPoint += ActivateCheckPoint;
        ScenesManager.instance.ActivateCheckPoint += life.AddLife;

        GameManager.instance.SubscribeBehaviours(this);
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
        if (IsPowerAttack) return;
  
        _AttackBox.enabled = true;
        _IsAttacking = true;
        StartCoroutine(Recharge(_AttackDuration, _AttackReload));
        OnAttack.Invoke(_AttackDuration, 0);
    }
    IEnumerator Recharge(float AttackD, float ReloadT)
    {
        var Wait = new WaitForSeconds(AttackD);
        yield return new WaitForSeconds(ReloadT);
        _AttackBox.enabled = false;
        _IsAttacking = false;
        //Debug.Log("Cant attack");
        yield return Wait;
        //Debug.Log("Can Attack again");
    }
    public void SuperAttack(float time, int NewDamage)
    {
        IsPowerAttack = true;
        int OldDamage = _Damage;
        _Damage = NewDamage;

        OnAttack.Invoke(time, 1);
        StartCoroutine(SuperTornadoActive(time, OldDamage));
    }
    IEnumerator SuperTornadoActive(float time, int OldDamage)
    {
        _AttackBox.enabled = true;
        _IsAttacking = true;
        yield return new WaitForSeconds(time);
        _AttackBox.enabled = false;
        IsPowerAttack = false;
        _Damage = OldDamage;
    }
    public void UpImpulse(float ForceUp)
    {
        var vel = _RigP.velocity;
        vel.y = 0;
        _RigP.velocity = vel;
        _RigP.AddForce(Vector3.up * ForceUp, ForceMode.VelocityChange);
    }
    public void Shoot()
    {
        RaycastHit hit;
        var _bulletObjectP = BulletFactory._instance.pool.GetObject();
        _bulletObjectP.transform.position = _firePoint.position;
    
        if (Physics.Raycast(_MainCamera.position, _MainCamera.forward, out hit, Mathf.Infinity))
        {
            
            _bulletObjectP.transform.forward = _MainCamera.forward;
            _bulletObjectP.target = hit.point;

            _bulletObjectP.hit = true;
        }
        else
        {
            _bulletObjectP.transform.forward = _MainCamera.forward;
            _bulletObjectP.target = _MainCamera.position + _MainCamera.forward * _MaxDistAir;
            _bulletObjectP.hit = true;
        }
        Vector3 camForward = _MainCamera.forward;
        camForward.y = 0;
        transform.forward = camForward;
    }
    public void RecieveBombDamage(int BombD)
    {
        life.RecieveHit(BombD);
    }
    public void CheckEnviroment()
    {
        if (Physics.Raycast(_RigP.transform.position, Vector3.down, jump.RayJumpDist, _Water))
            OnWater?.Invoke();
    }
    public void CheckPoint()
    {
        CheckPointPosition = transform.position;
        CheckPointRotation = transform.rotation;
        life.ResetLife();
    }

    public void ActivateCheckPoint()
    {
        _RigP.velocity = Vector3.zero;
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
        else if (I != null && _IsAttacking && I.CanBeHit) I.Activate();
    }
    public void DoubleJump(float time)
    {
        jump.IsDJumpActive = true;
        if (jump.IsDJumpActive)
            StartCoroutine(ForHowLong(time));
        //Debug.Log("Is JumpActive = " + jump.IsDJumpActive);
    }
    IEnumerator ForHowLong(float time)
    {
        yield return new WaitForSeconds(time);
        jump.IsDJumpActive = false;
        Debug.Log("DeActivate DoubleJump");
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

    public PlayerHealth GetHealth()
    {
        return life;
    }
}


[System.Serializable]
public class PlayerHealth
{
    public event Action<float> OnHealthChange;
    public event Action OnDeath;

    public int _MaxLife;
    public int _CurrentLife;

    public void RecieveHit(int damage)
    {
        _CurrentLife -= damage;
        _CurrentLife = Mathf.Max(0, _CurrentLife);
        if (_CurrentLife == 0) OnDeath?.Invoke();

        OnHealthChange?.Invoke(_CurrentLife);

        Debug.Log("AUCH " + _CurrentLife);
    }

    public void AddLife(int restore)
    {
        _CurrentLife += restore;
        _CurrentLife = Mathf.Min(_CurrentLife, _MaxLife);
        OnHealthChange?.Invoke(_CurrentLife);
    }

    public void ResetLife()
    {
        _CurrentLife = _MaxLife;
        OnHealthChange?.Invoke(_CurrentLife);
    }

}

[System.Serializable]
public class Glide
{
    public float DescendSpeed;
    public float SpeedHorizontal;
}

[System.Serializable]
public class PJump
{
    public float JumpForce;//11.3
    public float RayJumpDist;//0.87
    public bool IsJumping;
    public bool IsDJumpActive;

    public float FallMultiplier;//4.5
    public float VelocityFalloff;//8
}
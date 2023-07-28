using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem _AUCH;
    [SerializeField] ParticleSystem _WaterSplash;
    [SerializeField] ParticleSystem[] _ParticleRun;
    [SerializeField] GameObject[] _Attacks;
    [SerializeField] GameObject _Trails;
    [SerializeField] Transform _LifeContainer;
    bool _IsOnLand;
    Animator _animator;
    Image[] hearthIcons;

    private void Awake()
    {
        hearthIcons = _LifeContainer.GetComponentsInChildren<Image>();
        _animator = GetComponent<Animator>();
        _Trails.SetActive(false);
    }
    public void RecieveDamage(float currentHealth)
    {
        _AUCH.Play();
        for (int i = 0; i < hearthIcons.Length; i++)
            hearthIcons[i].enabled = (currentHealth > i);
        //CamaraScript.ShakeCamera(ShakeIntensity, ShakeTime);
    }
    public void Attack(float Time, int Num)
    {
        _animator.SetTrigger("Attack");
        StartCoroutine(AttackActiveT(Time, Num));
    }
    public IEnumerator AttackActiveT(float Time, int setD)
    {
        int currentI = 0;
        for (int i = 0; i < _Attacks.Length; i++)
        {//Modificar como el canvas manager
            if (i == setD)
            {
                _Attacks[setD].SetActive(true);
                currentI = setD;
            }
            else
                _Attacks[i].SetActive(false);
        }
        yield return new WaitForSeconds(Time);
        _Attacks[currentI].SetActive(false);
    }
  #region Bools
    public void SetRunning(bool IsRunning)
    {
        _animator.SetBool("IsRunning", IsRunning);
    }
    public void SetFlying(bool IsFlying)
    {
        _animator.SetBool("IsFlying", IsFlying);
        _Trails.SetActive(IsFlying);
    }
    public void SetFalling(bool IsFalling)
    {
        _animator.SetBool("IsFalling", IsFalling);
    }
    #endregion
  #region Triggers
    public void TriggerLand()
    {
        _animator.SetTrigger("Land");
        _IsOnLand = true;
    }
    public void Splash()
    {
        _WaterSplash.Play();
        _IsOnLand = false;
    }
    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
        _IsOnLand = false;
    }
    public void TriggerShoot()
    {
        _animator.SetTrigger("Shoot");
    }
    public void RunParticleLeft()
    {
        if(_IsOnLand == true)
            _ParticleRun[0].Play();
    }
    public void RunParticleRight()
    {
        if (_IsOnLand == true)
            _ParticleRun[1].Play();
    }
  #endregion
    public void IsDead()
    {
        GameManager.instance.Lose();
    }
}

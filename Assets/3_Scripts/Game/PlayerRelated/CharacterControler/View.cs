using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem _AUCH;
    [SerializeField] ParticleSystem _WaterSplash;
    [SerializeField] GameObject[] _Attacks;
    [SerializeField] Transform LifeContainer;
    Image[] hearthIcons;

    private void Awake()
    {
        hearthIcons = LifeContainer.GetComponentsInChildren<Image>();
    }
    public void RecieveDamage(float currentHealth)
    {
        _AUCH.Play();
        for (int i = 0; i < hearthIcons.Length; i++)
            hearthIcons[i].enabled = (currentHealth > i);
        //CamaraScript.ShakeCamera(ShakeIntensity, ShakeTime);
    }
    public void IsDead()
    {
        GameManager.instance.Lose();
    }
    public void Attack(float Time, int Num)
    {
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
    public void Splash()
    {
        _WaterSplash.Play();
    }
}

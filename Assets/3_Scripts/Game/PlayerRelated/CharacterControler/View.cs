using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem _AUCH;
    [SerializeField] ParticleSystem _WaterSplash;
    [SerializeField] Transform LifeContainer;
    public Image[] hearthIcons;
    [Header ("Camera Variables")]
    [SerializeField] float ShakeIntensity;
    [SerializeField] float ShakeTime;
    //[SerializeField] Camara CamaraScript;

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

    public void Splash()
    {
        _WaterSplash.Play();
    }
}

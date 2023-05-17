using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem auch;
    [Header ("Camera Variables")]
    [SerializeField] Camara CamaraScript;
    [SerializeField] float ShakeIntensity;
    [SerializeField] float ShakeTime = 1f;

    void Start()
    {
        
    }

    public void RecieveDamage()
    {
        auch.Play();
        CamaraScript.ShakeCamera(ShakeIntensity, ShakeTime);
    }

    public void IsDead()
    {
        GameManager.instance.Lose();
        Cursor.lockState = CursorLockMode.None;
    }
}

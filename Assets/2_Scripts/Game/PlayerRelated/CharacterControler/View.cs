using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem _AUCH;
    [SerializeField] ParticleSystem _WaterSplash;
    [Header ("Camera Variables")]
    [SerializeField] float ShakeIntensity;
    [SerializeField] float ShakeTime = 1f;
    Camera _MainCamera;
    Camara CamaraScript;

    void Start()
    {
        _MainCamera = Camera.main;
        CamaraScript = _MainCamera.GetComponent<Camara>();
        //_RigP = GetComponent<Rigidbody>();
    }

    public void RecieveDamage()
    {
        _AUCH.Play();
       // CamaraScript.ShakeCamera(ShakeIntensity, ShakeTime);
    }

    public void IsDead()
    {
        GameManager.instance.Lose();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Splash()
    {
        _WaterSplash.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem auch;
    [Header ("Camera Variables")]
    [SerializeField] float ShakeIntensity;
    [SerializeField] float ShakeTime = 1f;
    private Camera _MainCamera;
    private Camara CamaraScript;

    void Start()
    {
        _MainCamera = Camera.main;
        CamaraScript = _MainCamera.GetComponent<Camara>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] ParticleSystem auch;

    void Start()
    {
        
    }

    public void RecieveDamage()
    {
        auch.Play();
    }

    public void IsDead()
    {
        GameManager.instance.Lose();
        Cursor.lockState = CursorLockMode.None;
    }
}

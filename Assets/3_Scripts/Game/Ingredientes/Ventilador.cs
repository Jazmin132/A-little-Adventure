using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilador : MonoBehaviour
{
    [SerializeField] float SpeedUp;
    [SerializeField] float Maxcontador;
    Vector3 Vel;
    float contador;
    Rigidbody _OtherRIG;
    Coroutine _InFanCoroutine;

    private void Start()
    {
        contador = Maxcontador;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hi");
        if (other.TryGetComponent(out PlayerM I))
        {
            _OtherRIG = I.GetComponent<Rigidbody>();
            Vel = _OtherRIG.velocity;
            Vel.y = 0;
            _OtherRIG.velocity = Vel;

            _OtherRIG.AddForce(Vector3.up * 2f, ForceMode.VelocityChange);
            _InFanCoroutine = StartCoroutine(FLY());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        contador = Maxcontador;
        Vel = _OtherRIG.velocity;
        Vel.y = 7;
        _OtherRIG.velocity = Vel;
        if (other.TryGetComponent(out PlayerM I))
            StopCoroutine(_InFanCoroutine);
    }
    IEnumerator FLY()
    {
        var WaitForEndOfFrame = new WaitForEndOfFrame();
        while (contador > 0)
        {
            //Rig.AddForce(dir * speed, ForceMode.Acceleration);
            _OtherRIG.AddForce(Vector3.up * SpeedUp, ForceMode.Acceleration);
            contador -= Time.fixedDeltaTime;
            yield return WaitForEndOfFrame;
        }
    }
}

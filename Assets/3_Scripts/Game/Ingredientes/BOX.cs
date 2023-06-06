using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX : MonoBehaviour, IDamage
{
    public int _MaxLife;
    public int _CurrentLife;
    [SerializeField] GameObject _Recompenzas;
    private GameObject _RecompenzA;

    private void Start()
    {
        _CurrentLife = _MaxLife;
    }
    public void RecieveDamage(int damage)
    {
        _CurrentLife -= damage;
        if (_CurrentLife <= 0) Destroy();
    }
    public void Destroy()
    {
        var random = Random.Range(0, 4);
        for (int i = 0; i < random; i++)
        {
            Vector3 X = new Vector3(transform.position.x, transform.position.y, transform.position.z - (i * 2));
            _RecompenzA = Instantiate(_Recompenzas, transform.parent);
            _RecompenzA.transform.position = X;
        }
        Destroy(this.gameObject);
    }

}

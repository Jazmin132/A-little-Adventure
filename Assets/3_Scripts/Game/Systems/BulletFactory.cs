using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;

    [SerializeField] int _bulletStock = 5;

    public ObjectPool<Bullet> pool;

    public static BulletFactory _instance;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        pool = new ObjectPool<Bullet>(BulletCreator, Bullet.TurnOn, Bullet.TurnOff, _bulletStock);
    }
    //Funcion que contiene la logica de la creacion de la bala
    Bullet BulletCreator()
    {
        return Instantiate(_bulletPrefab);
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnBullet(Bullet b)
    {
        pool.ReturnObject(b);
    }
}

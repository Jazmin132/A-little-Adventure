using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyObjectPool<T>
{
    Func<T> _factoryMethod; 

    List<T> _currentStock; 
    bool _isDynamic; 

    Action<T> _turnOnCallback; 
    Action<T> _turnOffCallback;

    public EnemyObjectPool(Func<T> factoryMethod, Action<T> turnOn, Action<T> turnOff, int initialStock = 0, bool isDynamic = true)
    {
        _factoryMethod = factoryMethod;
        _turnOnCallback = turnOn;
        _turnOffCallback = turnOff;

        _isDynamic = isDynamic;

        _currentStock = new List<T>();

        for (int i = 0; i < initialStock; i++)
        {
            T obj = _factoryMethod();

            _turnOffCallback(obj);

            _currentStock.Add(obj);
        }
    }

    public T GetObject()
    {
        var result = default(T); 

        if (_currentStock.Count > 0) 
        {
            result = _currentStock[0]; 
            _currentStock.RemoveAt(0);
        }
        else if (_isDynamic) //Sino, pero si es dinamico
        {
            result = _factoryMethod();
        }

        _turnOnCallback(result); //Lo prendo con la logica del objeto

        return result;
    }

    public void ReturnObject(T obj)
    {
        _turnOffCallback(obj); 
        _currentStock.Add(obj);
    }
}

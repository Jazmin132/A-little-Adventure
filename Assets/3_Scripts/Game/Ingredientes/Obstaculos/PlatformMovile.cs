using UnityEngine;

public class PlatformMovile : MonoBehaviour
{
    [SerializeField] Transform[] _WayPoint;
    [SerializeField] GameObject _Platform;
    [SerializeField] float _Speed;
    [SerializeField] int _count;
    Vector3 _Dir;

    private void Start()
    {
        Debug.Log(_WayPoint.Length + " Length");
    }
    public void FixedUpdate()
    {
        _Dir = _WayPoint[_count].position - _Platform.transform.position;
        _Platform.transform.position += _Dir.normalized * _Speed * Time.fixedDeltaTime;

        if (Vector3.Distance(_Platform.transform.position, _WayPoint[_count].position) < 1f)
        {
            _count++;
            //ej _count 0-4  _WayPoint.Length 5 (empieza desde 1)
            if (_count >= _WayPoint.Length) 
                _count = 0;
        }
    }
}
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
        _Platform.transform.position += _Dir.normalized * _Speed;

        if (Vector3.Distance(_Platform.transform.position, _WayPoint[_count].position) < 1f)
        {
            _count++;
            _count = Mathf.Min(_count, _WayPoint.Length - 1);
            //Porqué no pasa a cero
            Debug.Log(_count == _WayPoint.Length - 1);
            Debug.Log(_count + " COUNT");
            if (_count == _WayPoint.Length - 1) _count = 0;
        }
    }
}
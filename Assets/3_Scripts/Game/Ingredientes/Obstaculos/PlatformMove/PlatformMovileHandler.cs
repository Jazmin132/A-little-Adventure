using UnityEngine;

public class PlatformMovileHandler : MonoBehaviour
{
    [SerializeField] Transform[] _WayPoint;
    [SerializeField] Transform _Platform;
    [SerializeField] float _Speed;
    int _count = 0;
    Vector3 _Dir;

    private void Start()
    {
        GameManager.instance.SubscribeBehaviours(this);
    }
    public void FixedUpdate()
    {
        _Dir = _WayPoint[_count].position - _Platform.transform.position;
        _Platform.position += _Dir.normalized * _Speed * Time.fixedDeltaTime;

        if (Vector3.Distance(_Platform.position, _WayPoint[_count].position) < 1f)
        {
            _count++;
            //_WayPoint.Length (empieza a contar desde 1)
            if (_count >= _WayPoint.Length) 
                _count = 0;
        }
    }
}
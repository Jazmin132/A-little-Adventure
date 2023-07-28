using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _UpForce;
    public bool PlayerDetected;
    MushoomHandler handler;
    bool _Trampolin = false;

    private void Start()
    {
        handler = GetComponentInParent<MushoomHandler>();
        if (handler == null) _Trampolin = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.UpImpulse(_UpForce);
            if (!_Trampolin)
            {
                PlayerDetected = true;
                handler.ActivateNextMushroom();
            }
        }
    }
}

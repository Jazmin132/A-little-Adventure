using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _UpForce;
    public bool PlayerDetected;
    MushoomHandler handler;

    private void Start()
    {
        handler = GetComponentInParent<MushoomHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerM P))
        {
            P.UpImpulse(_UpForce);
            PlayerDetected = true;
            handler.ActivateNextMushroom();
        }
    }
}

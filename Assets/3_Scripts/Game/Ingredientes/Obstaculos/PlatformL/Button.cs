using UnityEngine;

public class Button : MonoBehaviour
{
    PlatformLumineseHandler _Handler;

    void Start()
    {
        GameManager.instance.SubscribeBehaviours(this);
        _Handler = GetComponentInParent<PlatformLumineseHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {//Ponerselo a un botón
        if (other.GetComponent<PlayerM>() != null && _Handler.CanReset) _Handler.ResetPlatforms();
    }
}

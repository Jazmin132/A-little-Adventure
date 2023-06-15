using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    PlatformLumineseHandler _Handler;
    BoxCollider _AttackBox;

    void Start()
    {
        _Handler = GetComponentInParent<PlatformLumineseHandler>();
        _AttackBox = GetComponentInParent<BoxCollider>();
        _Handler.Win += Open;
    }

    void Open()
    {
        this.gameObject.SetActive(false);
        _AttackBox.enabled = false;
    }
}

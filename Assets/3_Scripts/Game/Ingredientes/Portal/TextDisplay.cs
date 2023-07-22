using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    Text _Mytext;
    Transform _MainC;
    Transform _ParentTranform;
    Portal _Portal;
    public Vector3 Offset;

    public TextDisplay SetOwner(Portal P)
    {
        _Portal = P;
        _Mytext = GetComponentInChildren<Text>();
        _Portal.CurrencyDisplay = _Mytext;

        return this;
    }
    private void Start()
    {
        _ParentTranform = _Portal.transform;
        _MainC = Camera.main.transform;
        transform.SetParent(CanvasPortalManager.instance.transform);
        transform.position = _ParentTranform.position + Offset;
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _MainC.transform.position);
    }
}

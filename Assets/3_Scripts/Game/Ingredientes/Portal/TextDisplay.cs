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
    Plane _Plane;
    public Vector3 Offset;

    public TextDisplay SetOwner(Portal P)
    {
        _Portal = P;
        _Mytext = GetComponentInChildren<Text>();
        _Portal.CurrencyDisplay = _Mytext;
        return this;
    }
    public TextDisplay SetPlaneOwner(Plane P)
    {
        _Plane = P;
        _Mytext = GetComponentInChildren<Text>();
        _Plane.CurrencyDisplay = _Mytext;
        return this;
    }
    private void Start()
    {
        _MainC = Camera.main.transform;
        if (_Plane != null)
        {
            _ParentTranform = _Plane.transform;
            Offset = new Vector3(0, 3.5f, 0);
        }
        else
            _ParentTranform = _Portal.transform;

        transform.SetParent(CanvasPortalManager.instance.transform);
        transform.position = _ParentTranform.position + Offset;
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _MainC.transform.position);
    }
}

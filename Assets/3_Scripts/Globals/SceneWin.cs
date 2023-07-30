using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWin : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.UnlockCursor();
    }
}

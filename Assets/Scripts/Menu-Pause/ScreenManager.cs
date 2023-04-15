using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    Stack<IScreen> _ScreenStack;
    static public ScreenManager instance;

    void Awake()
    {
        instance = this;
        _ScreenStack = new Stack<IScreen>();
    }
    public void Pop()
    {
        if (_ScreenStack.Count <= 1) return;//No hago nada
            _ScreenStack.Pop().Free();
        if (_ScreenStack.Count > 0)
            _ScreenStack.Peek().Activate();
    }
    public void Push(IScreen newScreen)
    {
        if (_ScreenStack.Count > 0)
            _ScreenStack.Peek().DeActivate();
        _ScreenStack.Push(newScreen);
        newScreen.Activate();
    }
}

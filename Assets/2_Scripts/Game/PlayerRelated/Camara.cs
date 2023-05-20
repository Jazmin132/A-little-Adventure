using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara : MonoBehaviour
{
    //[SerializeField] CinemachineVirtualCamera _VirtualCamera;
    [SerializeField] CinemachineFreeLook _VirtualCamera;
    private CinemachineBasicMultiChannelPerlin _PerlinNoise;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        //_PerlinNoise = _VirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        //_PerlinNoise.m_AmplitudeGain = 0f;
    }
    public void ShakeCamera(float intensity, float shaketime)
    {
        //_PerlinNoise.m_AmplitudeGain = intensity;
        StartCoroutine(Reset(shaketime));
    }
    IEnumerator Reset(float ShakeTime)
    {
        yield return new WaitForSeconds(ShakeTime);
        _PerlinNoise.m_AmplitudeGain = 0f;
    }
}

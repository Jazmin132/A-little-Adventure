using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara : MonoBehaviour
{
    CinemachineFreeLook _VirtualCamera;
    private CinemachineBasicMultiChannelPerlin _PerlinNoise;
    //ARREGLAR CAMERA SHAKE
    void Start()
    {
        _VirtualCamera = GetComponent<CinemachineFreeLook>();
        _PerlinNoise = _VirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Amplitude(0f);
    }
    
    public void ShakeCamera(float intensity, float shaketime)
    {
        Amplitude(intensity);
        StartCoroutine(Reset(shaketime));
    }
    
    IEnumerator Reset(float ShakeTime)
    {
        yield return new WaitForSeconds(ShakeTime);
        Amplitude(0f);
    }

    public void Amplitude(float ShakeTime)
    {
        _PerlinNoise.m_AmplitudeGain = ShakeTime;
    }
}

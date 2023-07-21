using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;

    [SerializeField] private CinemachineVirtualCamera DefaultCam;
    [SerializeField] private CinemachineVirtualCamera FollowChar;
    [SerializeField] public PlayerInput input;

    private CinemachineVirtualCamera currentCamera;
    private bool open;
    private float shakeTimer;
    private float goofy;
    private float shakeTimerDur;

    void Start()
    {
        cameras.Add(DefaultCam);
        cameras.Add(FollowChar);
        switchCamera(DefaultCam);
    }

    public void switchCamera(CinemachineVirtualCamera newCam)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] != newCam)
            {
                cameras[i].Priority = 10;
            }
            else
            {
                cameras[i].Priority = 20;
                currentCamera = cameras[i];
            }
        }
    }

    public void switchFollowTarget(GameObject newTarget)
    {
        FollowChar.Follow = newTarget.transform;
        FollowChar.LookAt = newTarget.transform;
    }

    public void cameraShake(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin Noise = FollowChar.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Noise.m_AmplitudeGain = intensity;
        goofy = intensity;
        shakeTimer = duration;
    }

    void Update()
    {
        if (input.openMap)
        {
            if (!open)
            {
                switchCamera(FollowChar);
            }
        else
            {
                switchCamera(DefaultCam);
            }
            open = !open;
        }

        ///// CAMERA SHAKE /////
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        }
        else if (shakeTimer <= 0)
        {
            //timer over
            CinemachineBasicMultiChannelPerlin Noise = FollowChar.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            Noise.m_AmplitudeGain = 0f;
            Mathf.Lerp(goofy, 0f, shakeTimer / shakeTimerDur);
        }
    }
}

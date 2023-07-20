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

    void Update()
    {
        if (input.openMap)
        {
            Debug.Log("kuybid");
            if (!open)
            {
                Debug.Log("dog");
                open = true;
                switchCamera(FollowChar);
            }
        else
            {
                Debug.Log("balls");
                open = false;
                switchCamera(DefaultCam);
            }
        }
    }
}

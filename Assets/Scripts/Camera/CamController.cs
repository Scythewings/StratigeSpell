using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;

    [SerializeField] private CinemachineVirtualCamera DefaultCam;
    [SerializeField] private CinemachineVirtualCamera FollowChar;

    private CinemachineVirtualCamera currentCamera;

    void Start()
    {
        cameras.Add(DefaultCam); 
        cameras.Add(FollowChar);
        switchCamera(DefaultCam);
    }

    public void switchCamera(CinemachineVirtualCamera newCam)
    {
        for (int i = 1; i > cameras.Count; i++)
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

    void Update()
    {

    }
}

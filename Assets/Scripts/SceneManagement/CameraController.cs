using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start() 
    {
        SetPlayerCameraFollow();
    }
    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}

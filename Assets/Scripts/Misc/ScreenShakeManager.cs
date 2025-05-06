using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    CinemachineImpulseSource ImpulseSource;
    protected override void Awake()
    {
        base.Awake();
        ImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void ShakeScreen()
    {
        ImpulseSource.GenerateImpulse();
    }
}

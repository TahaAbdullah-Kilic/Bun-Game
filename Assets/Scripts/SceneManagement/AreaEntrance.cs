using UnityEditor.SearchService;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] string TransitionName;
    void Start()
    {
        if (TransitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            Fader.Instance.FadeToClear();
        }
    }
}

using System.Buffers.Text;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }
    public void SetTransitionName(string sceneTransitionName)
    {
        SceneTransitionName = sceneTransitionName;
    }
}

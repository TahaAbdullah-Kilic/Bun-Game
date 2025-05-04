using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
    [SerializeField] string sceneTransitionName;
    float waitToLoadTime = 1f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            Fader.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine(waitToLoadTime));
        }
    }
    IEnumerator LoadSceneRoutine(float waitToLoadTime)
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(SceneToLoad);
    }
}

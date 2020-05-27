using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayModeButton : SingleGesture
{
  public string loadSceneName;
  public Animator animator;
  private AsyncOperation async;

  private void OnEnable()
  {
    this.Released += ReleasedHandler;
  }

  private void OnDisable()
  {
    this.Released -= ReleasedHandler;
  }

  private void ReleasedHandler(Vector2 position)
  {
    Debug.Log("release");
    async = SceneManager.LoadSceneAsync(loadSceneName);
    async.allowSceneActivation = false;
    animator.SetTrigger(loadSceneName);
    StartCoroutine("SceneLoad");
  }

  IEnumerator SceneLoad()
  {
    yield return new WaitForSeconds(2.5f);
    async.allowSceneActivation = true;
    yield return null;
  }
}

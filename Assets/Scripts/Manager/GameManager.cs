using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
  public float timeLimitSec = 120.0f;
  public List<PlayerController> playerList = new List<PlayerController>();
  public StageController stage;
  public ResultController result;

  float targetValue;

  void Start()
  {
    if (stage)
    {
      SetTargetValue();
    }
  }

  public void TogglePause()
  {
    if (Time.timeScale != 0)
    {
      Time.timeScale = 0f;
    }
    else
    {
      Time.timeScale = 1f;
    }
  }

  public void Restart()
  {
    LoadScene(SceneManager.GetActiveScene().name);
    Time.timeScale = 1f;    
  }

  public void LoadScene(string _sceneName)
  {
    SceneManager.LoadSceneAsync(_sceneName);
    Time.timeScale = 1f;
  }

  public void GameOver()
  {
    result.gameObject.SetActive(true);
  }

  public bool SetAnswer(float value)
  {
    if (value == targetValue)
    {
      targetValue = -1f;
      stage.Correct();
      Invoke("SetTargetValue", 1f);
      return true;
    }

    return false;
  }

  private void SetTargetValue()
  {
    targetValue = Random.Range(3, 18);
    stage.SetTargetText(targetValue.ToString());
  }
}

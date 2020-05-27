using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  private float timeLimitSec = 120.0f;
  public Image timeGauge;

  private float currentTime;

  void Start()
  {
    timeLimitSec = GameManager.Instance.timeLimitSec;
    currentTime = timeLimitSec;
  }

  void Update()
  {
    if (currentTime != 0)
    {
      CountDown();
    }
  }

  public void CountDown()
  {
    currentTime -= Time.deltaTime;
    if (currentTime <= 0.0f)
    {
      currentTime = 0.0f;
      GameManager.Instance.GameOver();
    }

    timeGauge.fillAmount = currentTime / timeLimitSec;
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
  public List<ShuffleText> scoreList = new List<ShuffleText>();

  void OnEnable()
  {
    for (int i = 0; i < scoreList.Count; i++)
    {
      scoreList[i].score = GameManager.Instance.playerList[i].score.ToString();
    }
  }
}

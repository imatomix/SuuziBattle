using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
  public Text targetText;
  public Animator animator;

  void Start()
  {
    if (GameManager.Instance.playerList.Count == 1)
    {
      animator.SetBool("multi", false);
    }
  }
  public void Correct()
  {
    animator.SetTrigger("correct");
  }

  public void SetTargetText(string text)
  {
    targetText.text = text;
  }
}

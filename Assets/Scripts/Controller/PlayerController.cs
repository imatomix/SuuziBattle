using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  [SerializeField]
  private HashSet<NumberController> numbers = new HashSet<NumberController>();
  [ReadOnly]
  public int score = 0;
  public Animator playerUI;

  public void Answer()
  {
    if (numbers.Count <= 0) return;

    float _value = 0;
    foreach(var nc in numbers){
      _value += nc.number;
    }

    bool response = GameManager.Instance.SetAnswer(_value);
    if (response)
    {
      score += numbers.Count * 10;
      ReleaseNumbers();
      playerUI.SetTrigger("correct");
    }
    else
    {
      playerUI.SetTrigger("incorrect");
    }
  }

  public void ReleaseNumbers()
  {
    foreach (NumberController number in numbers)
    {
      if (number.GetComponent<Rigidbody>())
      {
        number.GetComponent<Rigidbody>().AddExplosionForce(420f, transform.position, 10f);
      }
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    numbers.Add(collision.gameObject.GetComponent<NumberController>());
  }

  private void OnCollisionExit(Collision collision)
  {
    numbers.Remove(collision.gameObject.GetComponent<NumberController>());
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
  public Transform[] numberObjects;

  void Start()
  {
    Generate(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
  }

  public void Generate(int[] numbers)
  {
    foreach (int number in numbers)
    {
      Instantiate(
        numberObjects[number - 1],
        new Vector3(
          Random.Range(-1.2f, 1.2f),
          Random.Range(8f, 12f),
          Random.Range(-1.2f, 1.2f)),
        Random.rotation
      );
    }
  }
}


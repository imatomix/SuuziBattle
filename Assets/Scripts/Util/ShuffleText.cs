using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShuffleText : MonoBehaviour
{

  public string score;
  // 各表示開始のディレイ秒
  public float delay = 0.2f;
  // 処理間隔
  public float timeOut = 0.02f;
  // シャッフル演出用文字郡

  private string RandomCharacters = "1234567890";
  private Text textField;
  private float timeElapsed;
  private string text;
  private int length = 0;
  private bool display = false;

  void Start()
  {
    textField = gameObject.GetComponent<Text>();
    Shuffle();
  }

  public void Shuffle()
  {
    length = score.Length;
    textField.text = "";

    // 各表示開始のトリガー
    StartCoroutine("Starter");
  }

  void Update()
  {
    if (!display)
    {
      timeElapsed += Time.deltaTime; // 経過時間

      if (timeElapsed >= timeOut)
      {

        textField.text = GenerateRandomText(3);
        timeElapsed = 0.0f;
      }
    }
  }

  private IEnumerator Starter()
  {
    // delay秒分待機
    yield return new WaitForSeconds(delay);
    // 本番表示開始
    display = true;
    textField.text = score;

    yield return false;
  }

  private string GenerateRandomText(int length)
  {
    var stringBuilder = new System.Text.StringBuilder(length);
    var random = new System.Random();
    int position = 0;
    char c;

    for (int i = 0; i < length; i++)
    {
      position = random.Next(RandomCharacters.Length);
      c = RandomCharacters[position];
      stringBuilder.Append(c);
    }

    return stringBuilder.ToString();
  }
}
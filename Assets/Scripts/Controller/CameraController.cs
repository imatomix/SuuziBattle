using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CameraController : SingleGesture
{
  public enum moveTypeEnum
  {
    none = 0,
    horizontal = 1,
    vertical = 2,
    both = 3
  }
  public moveTypeEnum moveType;

  public float moveSpeed = 1.0f;
  private float flickPower = 10.0f;

  public bool limit = true;
  public Vector2 min = new Vector2(-8f, -4.5f);
  public Vector2 max = new Vector2(8, 4.5f);

  private Vector2 currentPoint, offset;
  private Vector2 throwDirection;
  private float screenRait;

  private void OnEnable()
  {
    this.Pressed += pressedHandler;
    this.Dragged += draggedHandler;
    this.DragEnded += dragEndedHandler;
  }

  private void OnDisable()
  {
    this.Pressed -= pressedHandler;
    this.Dragged -= draggedHandler;
    this.DragEnded -= dragEndedHandler;
  }

  void Start()
  {
    flickPower *= moveSpeed;

    Vector2 screenSize = Camera.main.ViewportToScreenPoint(new Vector2(1, 1));
    screenRait = screenSize.x / screenSize.y;
  }

  void Update()
  {
    if (transform.localPosition.x < min.x)
    {
      transform.localPosition = new Vector3(min.x, transform.localPosition.y, transform.localPosition.z);
    }
    else if (transform.localPosition.x > max.x)
    {
      transform.localPosition = new Vector3(max.x, transform.localPosition.y, transform.localPosition.z);
    }

    if (transform.localPosition.y < min.y)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, min.y, transform.localPosition.z);
    }
    else if (transform.localPosition.y > max.y)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, max.y, transform.localPosition.z);
    }
  }

  void pressedHandler(Vector2 position)
  {
    currentPoint = Camera.main.ScreenToViewportPoint(position);
  }

  void draggedHandler(Vector2 position)
  {
    position = Camera.main.ScreenToViewportPoint(position);
    throwDirection = (currentPoint - position);
    throwDirection = new Vector2(throwDirection.x * screenRait, throwDirection.y);


    switch (moveType)
    {
      case moveTypeEnum.horizontal:
        transform.Translate(throwDirection.x * moveSpeed, 0f, 0f);
        break;
      case moveTypeEnum.vertical:
        transform.Translate(0f, throwDirection.y * moveSpeed, 0f);
        break;
      case moveTypeEnum.both:
        transform.Translate(throwDirection.x * moveSpeed, throwDirection.y * moveSpeed, 0f);
        break;
      default:
        break;
    }
    currentPoint = position;

  }

  void dragEndedHandler(Vector2 position)
  {

    switch (moveType)
    {
      case moveTypeEnum.horizontal:
        throwDirection.y = 0f;
        GetComponent<Rigidbody>().AddForce(throwDirection * flickPower, ForceMode.Impulse);
        break;
      case moveTypeEnum.vertical:
        throwDirection.x = 0f;
        GetComponent<Rigidbody>().AddForce(throwDirection * flickPower, ForceMode.Impulse);
        break;
      case moveTypeEnum.both:
        GetComponent<Rigidbody>().AddForce(throwDirection * flickPower, ForceMode.Impulse);
        break;
      default:
        break;
    }
  }
}

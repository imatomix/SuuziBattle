using System.Collections;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NumberController : SingleGesture
{
  [ReadOnly]
  public int number = 1;
  public Rigidbody rigidbody;
  public Transform meshTransform;
  private float flickPower = 10f;
  private float hoverHeight = .25f;
  private Vector3 screenPoint, currentPoint, offset, throwDirection;
  private float delay = 20.0f;
  private Vector3 targetScale = Vector3.one;

  private bool isDragged = false;
  private bool isOnTable = false;

  private void OnEnable()
  {
    this.DragStarted += dragStartedHandler;
    this.Dragged += draggedHandler;
    this.DragEnded += dragEndedHandler;

    if (rigidbody == null)
    {
      rigidbody = GetComponent<Rigidbody>();
    }

    if (meshTransform == null)
    {
      meshTransform = transform.GetChild(0).gameObject.transform;
    }
  }

  private void OnDisable()
  {
    this.DragStarted -= dragStartedHandler;
    this.Dragged -= draggedHandler;
    this.DragEnded -= dragEndedHandler;
  }

  void FixedUpdate()
  {
    if (isDragged)
    {
      transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(currentPoint) + offset, delay * Time.deltaTime);
      throwDirection = (Camera.main.ScreenToWorldPoint(currentPoint) - transform.position + offset) * flickPower * GetComponent<Rigidbody>().mass;
    }
    /*
    else if (isOnTable)
    {
      Vector3 pos = transform.localPosition;
      pos.z = -2.95f;
      transform.localPosition = Vector3.Lerp(transform.localPosition, pos, delay * Time.deltaTime);
    }
    */
    meshTransform.localScale = Vector3.Lerp(meshTransform.localScale, targetScale, delay * Time.deltaTime);
  }

  private void dragStartedHandler(Vector2 position)
  {
    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    currentPoint = new Vector3(position.x, position.y, screenPoint.z);
    offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, screenPoint.z));

    isDragged = true;
    rigidbody.isKinematic = true;
  }

  void draggedHandler(Vector2 position)
  {
    currentPoint = new Vector3(position.x, position.y, screenPoint.z - 0.05f);
  }

  void dragEndedHandler(Vector2 position)
  {
    isDragged = false;
    rigidbody.isKinematic = false;
    rigidbody.AddForce(throwDirection, ForceMode.Impulse);
  }

  void OnTriggerStay(Collider other)
  {
    transform.rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, delay * Time.deltaTime);
  }

  void OnCollisionEnter(Collision collisionInfo)
  {
    if (collisionInfo.gameObject.tag == "Table")
    {
      isOnTable = true;
      targetScale = Vector3.one * 0.7f;
      transform.SetParent(collisionInfo.transform, true);
    }
  }

  void OnCollisionExit(Collision collisionInfo)
  {
    if (collisionInfo.gameObject.tag == "Table")
    {
      isOnTable = false;
      targetScale = Vector3.one;
      transform.SetParent(null, true);
    }
  }
}

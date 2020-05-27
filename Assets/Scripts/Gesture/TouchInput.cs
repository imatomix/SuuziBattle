using UnityEngine;

public class TouchInput : MonoBehaviour
{

  public float rayDistance = Mathf.Infinity;
  public LayerMask inputMask = -1;
  public int limitOfInputs = 5;

  private GestureBehaviour[] targets;

    void Awake() {
#if UNITY_EDITOR
        enabled = false;
#endif
        targets = new GestureBehaviour[limitOfInputs];
    }

  void Update()
  {
    for (int i = 0; i < Input.touchCount; i++)
    {
      Touch touch = Input.GetTouch(i);
      int id = touch.fingerId;

      switch (touch.phase)
      {
        case TouchPhase.Began:
          Ray ray = Camera.main.ScreenPointToRay(touch.position);
          RaycastHit hit = new RaycastHit();

          if (Physics.Raycast(ray, out hit, rayDistance, inputMask))
          {
            targets[id] = hit.collider.gameObject.GetComponentInParent(typeof(GestureBehaviour)) as GestureBehaviour;
          }
          else
          {
            targets[id] = Camera.main.gameObject.GetComponentInParent(typeof(GestureBehaviour)) as GestureBehaviour;
          }

          if (targets[id] != null)
          {
            targets[id].Began(id, touch.position);
          }
          break;
        case TouchPhase.Ended:
          if (targets[id] != null)
          {
            targets[id].Ended(id, touch.position);
          }
          break;
        case TouchPhase.Moved:
          if (targets[id] != null)
          {
            targets[id].Moved(id, touch.position);
          }
          break;
        case TouchPhase.Stationary:
          if (targets[id] != null)
          {
            targets[id].Stationary(id, touch.position);
          }
          break;
        case TouchPhase.Canceled:
          if (targets[id] != null)
          {
            targets[id].Canceled(id, touch.position);
          }
          break;
        default:
          break;
      }
    }
  }
}

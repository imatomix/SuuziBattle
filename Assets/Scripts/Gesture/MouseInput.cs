using System.Collections;
using UnityEngine;

public class MouseInput : MonoBehaviour {

    public float rayDistance = Mathf.Infinity;
    public LayerMask inputMask = -1;

    private GestureBehaviour target;
    private bool isBegan;
    private Vector3 position;

#if !UNITY_EDITOR
    private void Awake() {
        enabled = false;
    }
#endif

	void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast (ray, out hit, rayDistance, inputMask)) {
                target = hit.collider.gameObject.GetComponentInParent(typeof(GestureBehaviour)) as GestureBehaviour;
            } else {
                target = Camera.main.gameObject.GetComponentInParent(typeof(GestureBehaviour)) as GestureBehaviour;
            }

            if (target != null) {
                position = Input.mousePosition;
                target.Began(0, position);
                isBegan = true;
            }
        } else if (Input.GetMouseButtonUp (0)) {
            if(target != null) {
                target.Ended(0, Input.mousePosition);
            }
            isBegan = false;
        } else if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Right Click");
        }

        if (isBegan) {
            target.Stationary(0, Input.mousePosition);
            if(position != Input.mousePosition) {
                position = Input.mousePosition;
                target.Moved(0, position);
            }
        }
	}
}

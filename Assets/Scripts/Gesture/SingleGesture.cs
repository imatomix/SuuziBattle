using UnityEngine;

public class SingleGesture : MonoBehaviour, GestureBehaviour {

    public delegate void Gesture(Vector2 position);
    public event Gesture Pressed, Released, Exit, Enter;
    public event Gesture DragStarted, Dragged, DragEnded;
    public event Gesture HoldStarted, Holded, HoldEnded;

    private float rayDistance = Mathf.Infinity;
    private Vector2 startPosition;
    private bool isMoved = false;
    private bool isHolded = false;
    private bool isHoldEnd = false;
    private bool isExit = false;
    private int id = -1;

	void Awake () {
        GameObject input = GameObject.Find("InputManager");
        MouseInput mouseInput = input.GetComponent<MouseInput>();
        TouchInput touchInput = input.GetComponent<TouchInput>();

        if (mouseInput.enabled) {
            rayDistance = mouseInput.rayDistance;
        } else {
            rayDistance = touchInput.rayDistance;
        }
	}
	
    public void Began(int id, Vector2 position) {
        if (this.id != -1) return;

        this.id = id;
        startPosition = position;

        if (Pressed != null) {
            Pressed(position);
        }
    }

    public void Ended(int id, Vector2 position) {
        if (id != this.id) return;

        if (Released != null) {
            if (!isMoved) {
                Ray ray = Camera.main.ScreenPointToRay(position);
                RaycastHit hit = new RaycastHit();

                if(Physics.Raycast (ray, out hit, rayDistance)) {
                    if(transform.gameObject == hit.collider.gameObject) {
                        Released(position);
                    }
                }
            }
        }

        if (isMoved) {
            if(DragEnded != null) {
                DragEnded(position);
            }
            isMoved = false;
        }

        if (isHolded) {
            if(HoldEnded != null) {
                HoldEnded(position);
            }
            isHolded = false;
        }

        if (!isHoldEnd) {
            isHoldEnd = true;
        }

        this.id = -1; // reset
            
    }

    public void Moved(int id, Vector2 position) {
        if (id != this.id) return;

        if (DragStarted != null || Dragged != null) {
            if (!isMoved) {
                if (DragStarted != null) {
                    DragStarted(startPosition);
                }
                isMoved = true;
            } else {
                if (Dragged != null) {
                    Dragged(position);
                }
            }
        } else {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit = new RaycastHit();

            if(Physics.Raycast (ray, out hit, rayDistance)) {
                if(transform.gameObject != hit.collider.gameObject) {
                    if(!isExit && Exit != null) {
                        Exit(position);
                    }
                    isExit = true;

                    if (isHolded && HoldEnded != null) {
                        HoldEnded(position);
                    }
                    isHolded = false;
                } else {
                    if(isExit && Enter != null) {
                        Enter(position);
                    }
                    isExit = false;

                    if(!isHolded && !isHoldEnd) {
                        isHolded = true;
                    }
                }
            } else {
                if (!isExit && Exit != null) {
                    Exit(position);
                }
                isExit = true;

                if (isHolded && HoldEnded != null) {
                    HoldEnded(position);
                }
                isHolded = false;
            }
        }
    }

    public void Stationary(int id, Vector2 position) {
        if (id != this.id) return;

        if (!isHolded && isHoldEnd) {
            if(HoldStarted != null) {
                HoldStarted(position);
            }
            isHolded = true;
            isHoldEnd = false;
        } else if (isHolded && !isHoldEnd) {
            if(Holded != null) {
                Holded(position);
            }
        }
    }

    public void Canceled(int id, Vector2 position) {
        Debug.Log(id + ": Canceled");
    }

}

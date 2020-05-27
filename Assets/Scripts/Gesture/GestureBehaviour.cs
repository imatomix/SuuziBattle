using UnityEngine;

public interface GestureBehaviour {
    void Began(int id, Vector2 position);
    void Ended(int id, Vector2 position);
    void Moved(int id, Vector2 position);
    void Stationary(int id, Vector2 position);
    void Canceled(int id, Vector2 position);
}
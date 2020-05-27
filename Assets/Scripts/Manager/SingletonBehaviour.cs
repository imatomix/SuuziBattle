using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
  protected static T instance;

  public static T Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<T>();
        if (instance == null)
        {
          Debug.LogWarning(typeof(T) + " not found.");
        }
      }
      return instance;
    }
  }

  protected virtual void Awake()
  {
    CheckInstance();
  }

  protected bool CheckInstance()
  {
    if (this == Instance) return true;

    Destroy(gameObject);
    return false;
  }
}

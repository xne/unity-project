using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<T>();

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
            return;

        if (instance == null)
            instance = this as T;
        else
            Destroy(gameObject);
    }
}

using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
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
        {
            instance = this as T;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}

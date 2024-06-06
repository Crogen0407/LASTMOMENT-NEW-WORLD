using UnityEngine;

public class MonoDontDestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(instance.gameObject);
            }
            return instance;
        }
    } 
}
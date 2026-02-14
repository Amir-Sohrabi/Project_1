using UnityEngine;

namespace _Scripts.Utils
{
    /// <summary>
    /// Thread-safe, scene-safe generic singleton for MonoBehaviours.
    /// Automatically creates instance if none exists.
    /// Prevents duplicates and handles application quit state.
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object Lock = new object();
        private static bool _isQuitting;

        public static T Instance
        {
            get
            {
                if (_isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance of {typeof(T)} requested after application quit.");
                    return null;
                }

                lock (Lock)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = FindAnyObjectByType<T>();

                    if (_instance != null)
                        return _instance;

                    var singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T)} detected. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
}
using UnityEngine;

namespace _Scripts.Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindAnyObjectByType<T>();
                
                return _instance;
            }
        }
    }
}
using UnityEngine;

namespace LiteFramework
{
    public class DontDestoryOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
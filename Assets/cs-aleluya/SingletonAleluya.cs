/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class SingletonAleluya<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown_aleluya = false;
    private static object m_Lock_aleluya = new object();
    private static T m_Instance_aleluya;
 
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance_aleluya
    {
        get
        {
            if (m_ShuttingDown_aleluya)
            {
                Debug.LogWarning("Aleluya - [Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }
 
            lock (m_Lock_aleluya)
            {
                if (m_Instance_aleluya == null)
                {
                    // Search for existing instance.
                    m_Instance_aleluya = (T)FindObjectOfType(typeof(T));
 
                    // Create new instance if one doesn't already exist.
                    if (m_Instance_aleluya == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject_aleluya = new GameObject();
                        m_Instance_aleluya = singletonObject_aleluya.AddComponent<T>();
                        singletonObject_aleluya.name = typeof(T).ToString() + " (Singleton)";
 
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject_aleluya);
                    }
                }
 
                return m_Instance_aleluya;
            }
        }
    }
 
 
    private void OnApplicationQuit()
    {
        m_ShuttingDown_aleluya = true;
    }
 
 
    private void OnDestroy()
    {
        m_ShuttingDown_aleluya = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton pattern creating an instance for the given type
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    /// <summary>
    /// Making sure only one instance is present in the game
    /// </summary>
    protected virtual void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this as T;
        }
    }
}
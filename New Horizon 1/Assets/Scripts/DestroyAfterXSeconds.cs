using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to any Gameobject that will be destroyed X seconds after instantiations
/// </summary>
public class DestroyAfterXSeconds : MonoBehaviour
{
    public float TimeBeforeDestroying;
    void Start()
    {
        Destroy(gameObject, TimeBeforeDestroying);
    }
}

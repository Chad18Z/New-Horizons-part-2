using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// System for triggering game events
/// </summary>
public class trigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            EventManager.Instance.TriggerEvent("NextStep", null);
            Destroy(gameObject);
        }
    }
}

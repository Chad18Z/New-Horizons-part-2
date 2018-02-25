using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    static Quaternion rot;

	// Use this for initialization
	void Start () {
		
	}
    public static Quaternion GetRotation
    {
        get { return rot; }
    }
        
	
	//
	void Update ()
    {
        // We need to check how close the mouse cursor is to the player
        // When the mouse cursor gets too close to the player, it causes strange behavior
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (diff.magnitude > 5)
        {
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
            rot = transform.rotation;
        }
    }
}

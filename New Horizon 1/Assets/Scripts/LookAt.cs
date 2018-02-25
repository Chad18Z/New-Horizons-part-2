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
        Vector3 diff = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (diff.magnitude > 5) //need to smoothen out the rotation
        {
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            rot = Quaternion.Euler(0f, 0f, rot_z + 90);
            transform.rotation = rot;
        }
    }
}

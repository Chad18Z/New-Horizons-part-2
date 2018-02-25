using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //// if a beam is already instantiated, then destroy it
        //if (GameObject.FindGameObjectWithTag("cyto") != null)
        //{
        //    Destroy(GameObject.FindGameObjectWithTag("cyto"));
        //}
    }
	
	// Destroy this object if the player is not holding down the left mouse button
	void Update () {

        if (!Input.GetMouseButton(0))
        {
            Destroy(gameObject);
        }
		
	}
}

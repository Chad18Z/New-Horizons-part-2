using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class will wait for the player to press the left mouse button, and then raycast and draw
/// line (beam) from player to the target
/// ***Attach this script to the player object!!!
/// </summary>
public class CytoBeamFirer : MonoBehaviour {

    [SerializeField]
    GameObject cytoBeam;


    // Use this for initialization
    void Start () {

 
    }
	
	// listen for mouse button press
	void FixedUpdate () {
      
        if (Input.GetMouseButton(0))
        {
            GameObject beam = Instantiate(cytoBeam, gameObject.transform);
            LineRenderer toxin = beam.GetComponent<LineRenderer>();
            toxin.SetPosition(0, transform.position);
            toxin.SetPosition(1, Vector3.zero);
            toxin.enabled = true;
        }		
	}
}

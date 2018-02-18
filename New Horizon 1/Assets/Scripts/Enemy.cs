using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float massMultiplier = 25;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().mass = massMultiplier * (Mathf.PI * Mathf.Pow((transform.localScale.x / 2), 2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

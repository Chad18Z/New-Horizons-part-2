using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cytoblobScr : MonoBehaviour {


    [SerializeField]
    GameObject cytoSplash;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag != "arrow" && collisionTag != "Player" && collisionTag != "cytoMount")
        {
            GameObject splash = Instantiate(cytoSplash);
            ContactPoint2D[] points = collision.contacts;
            splash.transform.position = points[0].point;
            Destroy(gameObject); // for now, just destroy yourself when you collide with anything
        }

    }

    // when blast leaves screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

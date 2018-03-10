using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cytoblobScr : MonoBehaviour {


    [SerializeField]
    GameObject cytoSplash;

    Vector3 normalScale;

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
            if (points.Length > 0)
            {
                splash.transform.position = points[0].point;
            }
            else
            {
                splash.transform.position = collision.transform.position;
            }
            

            normalScale = transform.localScale; // because we transferring to a parent object, we must reset the local scale of this object.
            splash.transform.parent = collision.transform; // stick to whatever you just collided with
            splash.transform.localScale = normalScale;
            Destroy(gameObject); // for now, just destroy yourself when you collide with anything
        }

    }

    // when blast leaves screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

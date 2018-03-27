using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Need to make cytoblobs accumulate at the center of the badguy, how to do this?
/// </summary>
public class cytoblobScr : MonoBehaviour {


    [SerializeField]
    GameObject cytoSplash;

    //GameObject coll;  // reference to the game object that the cytoblob collided with

    Rigidbody2D rb; // the cytoblob's rigidbody

    Vector3 normalScale;

    float speed = 15.0f; // how fast the cytoblob will move towards the center of the badguy


	// Use this for initialization
	void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
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
                Destroy(gameObject);
            }
    }

    // when blast leaves screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

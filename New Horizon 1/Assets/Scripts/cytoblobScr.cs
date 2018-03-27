using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cytoblobScr : MonoBehaviour {


    [SerializeField]
    GameObject cytoSplash;

    GameObject coll;  // reference to the game object that the cytoblob collided with

    Rigidbody2D rb; // the cytoblob's rigidbody

    Vector3 normalScale;

    float speed = 20.0f; // how fast the cytoblob will move towards the center of the badguy
    CytoState state;

	// Use this for initialization
	void Start () {

        state = CytoState.toBadguy;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case CytoState.toBadguy:
                break;
            case CytoState.toCenter:
                ToCenter();
                break;
            case CytoState.atCenter:
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // only check for collisions while enroute to badguy
        if (state == CytoState.toBadguy)
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
                coll = collision.gameObject; // get reference to object that this cytoblob collided with
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true; // turn the collider off so that the blob can penetrate the enemy

                rb.velocity = Vector3.zero; // stop the blob
                SetLayer(); // lower sorting layer of this cytoblob so that it is rendered behind the badguy
                state = CytoState.toCenter; // change the cytoblob's state to: Enroute to center of badguy
            }
        }

    }

    // when blast leaves screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// This method will guide the cytoblob to the center of the badguy after the initial collision between the two objects
    /// </summary>
    void ToCenter()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, coll.transform.position, step);
    }

    /// <summary>
    /// This method will change the rendering layer of the particle system attached to the gameobject which this cytoblob collided with
    /// </summary>
    /// <param name="obj"></param>
    void SetLayer()
    {      
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().sortingOrder = 0;   // this makes the cytoblob render behind the bad guy    
    }
}

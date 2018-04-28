using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is poorly named, it actually controls the behavior the dummy T-Cells
/// </summary>
public class dummyBubbles : MonoBehaviour {

    ParticleSystem bubbles;
    Rigidbody2D rb;
    Vector3 destination;
    bool destinationSet = false;
    float idleStartSpeed = 3f;
    float idleRateOverTime = 7f;
    float maxSpeed = 15f;
    float maxRateOverTime = 115f;


    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (destinationSet)
        {
            rb.AddForce(destination * 50f, ForceMode2D.Impulse);
            destinationSet = false;
        }
    }

    public Vector3 SetDestination
    {
        set
        {
            destination = value;
            destinationSet = true;
        }
        get { return destination; }
    }

    public void ApplyNewRates()
    {

    }
}

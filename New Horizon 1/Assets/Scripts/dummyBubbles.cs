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

    GameObject player;

    public bool secondRoom;
    // Use this for initialization
    void Start () {

        bubbles = GameObject.FindGameObjectWithTag("dummyBubbles").GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (destinationSet)
        {
            float forceToApply = 50f;
            if (secondRoom) forceToApply = 75f;
            rb.AddForce((Vector2)destination * forceToApply, ForceMode2D.Impulse);
            destinationSet = false;
        }

        if (secondRoom && Vector3.Distance(gameObject.transform.position, player.transform.position) < 60)
        {
            rb.velocity = new Vector2(0, 0);
            //ApplyNewRates();
            secondRoom = false;
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
        // Produce bubbles first
        var emmision = bubbles.emission;
        float rate = emmision.rateOverTime.constant;
        // update values in the particle system
        emmision.rateOverTime = 7f;
    }
}

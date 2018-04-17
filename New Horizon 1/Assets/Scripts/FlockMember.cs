using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockMember : Cell {

    // this will hold the Flock Controller
    GameObject manager;
    Vector2 location = Vector2.zero; // this is Public so that other members can see it
    Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;
    Rigidbody2D rb;
    float neighborDistance;

    [SerializeField]
    GameObject damageTextObject;

    float damageMultiplier = .4f; // multiplied times the magnitude of the velocity of collision with cytoblob

    // Use this for initialization
    protected override void Start ()
    {
        maxHealth = health;

        cellInfo = gameObject.AddComponent(typeof(UICellInfo)) as UICellInfo;

        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, 
            this.gameObject.transform.position.y);

        rb = gameObject.GetComponent<Rigidbody2D>();
        neighborDistance = manager.GetComponent<FlockController>().distanceToNeighbor;
	}
    // Update is called once per frame
    protected override void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            RemoveFromFlockMembers();
        }

        Flock();
        goalPos = manager.transform.position; // this will change later. This will become the location where the flock is attacking.
    }

    /// <summary>
    /// share this object's location
    /// </summary>
    public Vector2 GetLocation
    {
        get { return location; }
    }
    /// <summary>
    /// share this object's velocity
    /// </summary>
    public Vector2 GetVelocity
    {
        get { return velocity; }
    }
    /// <summary>
    /// makes manager available to outside objects
    /// </summary>
   public GameObject SetManager
    {
        set { manager = value; }
        get { return manager; }
    }


    /// <summary>
    /// Provides vector from current location to target location
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    Vector2 Seek(Vector2 target)
    {
        return (target - location);
    }

    /// <summary>
    /// This will apply force to the member so that it follows the flocking rules
    /// </summary>
    /// <param name="f"></param>
    void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0); // must convert from 2D vector to 3D vector

        // clamp force to always be less than or equal to maximum allowed force
        if (force.magnitude > manager.GetComponent<FlockController>().maxForce)
        {
            force = force.normalized; // reduce to unit vector
            force *= manager.GetComponent<FlockController>().maxForce; // set equal to maximum allowed force
        }
        rb.AddForce(force, ForceMode2D.Impulse); // apply the calculated force to this member object
    }

    /// <summary>
    /// try to stay aligned with the rest of the flock towards the goal
    /// </summary>
    /// <returns></returns>
    Vector2 Align()
    {
        Vector2 sum = Vector2.zero;
        int counter = 0;
        foreach (GameObject other in manager.GetComponent<FlockController>().flockMembers)
        {
            if (other == this.gameObject || other == null) continue; // don't try to align with ourself!
            float distance = Vector2.Distance(location, other.GetComponent<FlockMember>().GetLocation);
            if (distance < neighborDistance)
            {
                sum += other.GetComponent<FlockMember>().GetVelocity;
                counter++;
            }
        }
        if (counter > 0)
        {
            sum /= counter;
            return sum - velocity;
        }
        return Vector2.zero;

    }
    /// <summary>
    /// Stay with the group!
    /// </summary>
    /// <returns></returns>
    Vector2 Cohesion()
    {
        Vector2 sum = Vector2.zero;
        int counter = 0;
        foreach (GameObject other in manager.GetComponent<FlockController>().flockMembers)
        {
            if (other == this.gameObject || other == null) continue;

            float distance = Vector2.Distance(location, other.GetComponent<FlockMember>().GetLocation);
            if (distance < neighborDistance)
            {
                sum += other.GetComponent<FlockMember>().GetLocation;
                counter++;
            }
        }
        if (counter > 0)
        {
            sum /= counter;
            return Seek(sum);
        }
        return Vector2.zero;
    }

    void Flock()
    {
        location = this.transform.position;
        velocity = rb.velocity;

        if (Random.Range( 0, 50) <= 1) // this member will flock based on a percentage chance
        {
            // build the force vector
            Vector2 align = Align();
            Vector2 cohesion = Cohesion();
            Vector2 gl;
            if (manager.GetComponent<FlockController>().SeekGoal)
            {
                gl = Seek(goalPos);
                currentForce = gl + align + cohesion;
            }
            else
                currentForce = align + cohesion;

            currentForce = currentForce.normalized; // convert to unit vector
        }
        // apply the force vector to the member
        ApplyForce(currentForce);
    }

    /// <summary>
    /// Check for collision with cytoblob. This formula might need some refinement
    /// </summary>
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.CompareTag("cytoBlob"))
        {
            float tempHealth = (coll.relativeVelocity.magnitude * damageMultiplier) / gameObject.transform.localScale.x;
            health -= tempHealth;
            int tHealth = (int)tempHealth;
            damageTextObject.GetComponent<Text>().text = tHealth.ToString();
            GameObject part = Instantiate(damageTextObject);
            damageTextObject.transform.position = coll.transform.position;
        }
    }

    private void RemoveFromFlockMembers()
    {
        GameObject[] members = manager.GetComponent<FlockController>().flockMembers;
        for (int i = 0; i < members.Length; i++)
        {
            if (gameObject == members[i])
            {
                members[i] = null;
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour {

    // this array will hold all instantiated flock members' object IDs
    public GameObject[] flockMembers;

    // Be sure to drag the single member prefab into this slot in the inspector!
    [SerializeField]
    GameObject memberPrefab;

    // Set this variable in the inspector, it controls how many members are spawned with this flock
    [SerializeField]
    int numberOfMembers;

    // size of space that all members of this flock will spawn within
    Vector3 range = new Vector3(5, 5, 5);

    // when one member gets this close to another, they become mutually-attracted
    [Range(0, 200)]
    public int distanceToNeighbor = 50;

    // max force that can ever be applied to any member. Higher = more aggressive movement
    [Range(0, 5)]
    public float maxForce = .5f;

    // max velocity of any member
    [Range(0, 10)]
    public float maxVelocity = 2.0f;

    // List of patrolling waypoints for flock, must be set in inspector
    public GameObject[] Waypoints;

    private FlockManager flockManager;
    private FlockControllerWaypoint currentWaypoint;
    private Coroutine waitingCoroutine = null;

    private GameObject playerObject;
    private float playerSightRange = 10f;

    //whether or not the flock is seeking a goal position
    bool seekGoal = true;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        flockManager = new FlockManager();
        foreach (GameObject go in Waypoints)
        {
            flockManager.AddWaypoint(go.GetComponent<FlockControllerWaypoint>());
        }

        flockMembers = new GameObject[numberOfMembers];
        for (int i = 0; i < numberOfMembers; i++)
        {
            Vector3 memberStartPosition = new Vector3(Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(0, 0));
            flockMembers[i] = Instantiate(memberPrefab, this.transform.position + memberStartPosition,
                Quaternion.identity) as GameObject;
            flockMembers[i].GetComponent<FlockMember>().SetManager = this.gameObject;
        }

        currentWaypoint = flockManager.GetNextWaypoint();
    }

    /// <summary>
    /// Update flock waypoint
    /// </summary>
    void Update ()
    {
        // check distance to player - if within certain distance, set pos to player
        if (Vector2.Distance(CenterOfFlock(), playerObject.transform.position) < playerSightRange)
        {
            maxVelocity = 30f;
            transform.position = playerObject.transform.position;
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
                waitingCoroutine = null;
            }
        }

        // otherwise check if approximately at waypoint, if so set next waypoint
        else if (Vector2.Distance(CenterOfFlock(), transform.position) < 1f && waitingCoroutine == null)
        {
            currentWaypoint = flockManager.GetNextWaypoint();
            waitingCoroutine = StartCoroutine(WaypointPause());
        }

        // if player just got out of sight, then start patrolling again
        else
        {
            maxVelocity = 6f;
            transform.position = currentWaypoint.Position;
        }
    }

    private IEnumerator WaypointPause()
    {
        float t = 0;
        while (t <= currentWaypoint.WaitTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = currentWaypoint.Position;
        waitingCoroutine = null;
    }

    private Vector2 CenterOfFlock()
    {
        Vector2 temp = new Vector2();
        for (int i = 0; i < flockMembers.Length; i++)
        {
            if (flockMembers[i] != null)
                temp += (Vector2)flockMembers[i].transform.position;
        }
        return temp / flockMembers.Length;
    }

    //returns whether or not the flock is seeking a goal position
    public bool SeekGoal
    {
        get { return seekGoal; }
    }

}

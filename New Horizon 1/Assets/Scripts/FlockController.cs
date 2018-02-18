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
    [Range(0, 2)]
    public float maxForce = .5f;

    // max velocity of any member
    [Range(0, 5)]
    public float maxVelocity = 2.0f;

    //whether or not the flock is seeking a goal position
    bool seekGoal = true;

	// Use this for initialization
	void Start () {
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
    }

    /// <summary>
    /// This function is for demo purposes only. It allows the user to move the flock around the screen
    /// Right-Click to move the flock around the screen
    /// </summary>
    void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    //returns whether or not the flock is seeking a goal position
    public bool SeekGoal
    {
        get { return seekGoal; }
    }

}

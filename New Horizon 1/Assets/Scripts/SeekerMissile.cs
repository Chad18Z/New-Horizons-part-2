using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMissile : MonoBehaviour {

    public GameObject target;

    Vector3 direction;
    float currentZRotation;
    float initialDistanceToTarget;
    Vector3 initialPosition;

    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float speed = 1f;


    // Use this for initialization
    void Start ()
    {
        initialDistanceToTarget = (target.transform.position - transform.position).magnitude;
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.Translate(Vector3.right * speed * Time.deltaTime);


        float distanceTravelled = (initialPosition - transform.position).magnitude;
        if (distanceTravelled > initialDistanceToTarget)
        {
            Destroy(gameObject);
        }
    }
}

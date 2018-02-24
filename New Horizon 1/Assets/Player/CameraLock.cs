using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour {

    [SerializeField] GameObject lockTarget;
    Vector2 lockTargetAndMyDifference;
    Vector3 positionToChange;
    float amountToChangeBy = 0.5f;

    void Start()
    {
        if (lockTarget == null)
        {
            Debug.LogError("Whoa! You forgot to put the player as the lock target on the camera, dummy.");
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        lockTargetAndMyDifference = new Vector2(lockTarget.transform.position.x - transform.position.x, lockTarget.transform.position.y - transform.position.y);

        if (lockTargetAndMyDifference.magnitude > 0.05f)
        {
            positionToChange = new Vector3(amountToChangeBy * lockTargetAndMyDifference.x, amountToChangeBy * lockTargetAndMyDifference.y, 0);
            transform.position += positionToChange;
        }
        else
        {
            // Set the x and y coordinates of this object to be the targets x and y coordinate
            transform.position = new Vector3(lockTarget.transform.position.x, lockTarget.transform.position.y, transform.position.z);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour {

    [SerializeField] float telescopeRange;
    GameObject lockTarget;
    Vector2 lockTargetAndMyDifference;
    Vector3 positionToChange;
    Vector3 scopedTargetPosition;
    float amountToChangeBy = 0.3f;
    float distanceToUpperCorner;
    float startingZ;
    Vector3 screenCenter;
    bool isTelescoping;

    void Start()
    {
        // Assign player as target to lock onto
        lockTarget = GameObject.FindGameObjectWithTag("Player");
        if (lockTarget == null) Debug.LogError("Whoa! You forgot to tag the player as 'Player', dummy.");

        startingZ = transform.position.z;
        
        // Gets distance to upper corner of screen
        Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        distanceToUpperCorner = (upperRightCornerWorld - lockTarget.transform.position).magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition;
        
        // If the middle mousebutton is down...
        if (isTelescoping)
        {
            // ...set the camera's target position to be somewhere based off of the mouse distance from the screen's center
            Vector3 mousePositionDiffFromCenterScreen = Input.mousePosition - screenCenter;
            Vector3 positionToBeFromPlayer = (mousePositionDiffFromCenterScreen / distanceToUpperCorner) * telescopeRange;
            scopedTargetPosition = lockTarget.transform.position + positionToBeFromPlayer;
            scopedTargetPosition = new Vector3(scopedTargetPosition.x, scopedTargetPosition.y, startingZ);

            // Set the camera's target position to be where the telescope told it
            targetPosition = scopedTargetPosition;
        }
        // Otherwise, make camera's target position just be the player
        else
        {
            targetPosition = lockTarget.transform.position;
        }

        // Get the difference from where the camera is and where it wants to be
        lockTargetAndMyDifference = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);

        // If the camera is a noticeable distance away from it's target...
        if (lockTargetAndMyDifference.magnitude > 0.05f)
        {
            // Change position of camera by a fraction of how far the target is
            positionToChange = new Vector3(amountToChangeBy * lockTargetAndMyDifference.x, amountToChangeBy * lockTargetAndMyDifference.y, 0);
            transform.position += positionToChange;
        }
        // Otherwise...
        else
        {
            // Set the x and y coordinates of this object to be the targets x and y coordinate
            transform.position = new Vector3(targetPosition.x, targetPosition.y, startingZ);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isTelescoping = !isTelescoping;
        }
    }
}

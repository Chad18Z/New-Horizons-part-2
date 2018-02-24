using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveShifter : MonoBehaviour {

    [Range(0f, 1f)] [SerializeField] float perspectiveShift = 0.20f;

    Vector3 startingCamPosition;
    Vector3 startingThisPosition;
    Vector3 currentCameraOffset;

    void Start()
    {
        // Set where the camera started and where this object started
        startingCamPosition = Camera.main.transform.position;
        startingThisPosition = transform.position;
    }

    void Update()
    {
        // Get how far away the camera is from where it started
        currentCameraOffset = Camera.main.transform.position - startingCamPosition;

        // Offset this by that same vector, but to a lesser magnitude
        transform.position = startingThisPosition + currentCameraOffset * perspectiveShift;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for the main game level
/// </summary>
public class GameManager : AManager
{
    protected override void Start()
    {
        InitialRoom();
        EventManager.Instance.StartListening("NextStep", NextStep);
    }

    protected override void Update()
    {
        // For etsting purposes, this event can be called from anywhere
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.Instance.TriggerEvent("NextStep", null);
        }

        // Update logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                Debug.Log("On step 0");
                break;
            case 1:
                Debug.Log("On step 1");
                break;
        }
    }

    protected override void NextStep(EventParam e)
    {
        Debug.Log("Going to next step");

        // Cleanup logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                Debug.Log("Cleanup step 0");
                break;
            case 1:
                Debug.Log("Cleanup step 1");
                break;
        }

        currStep++;

        // Start logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                Debug.Log("Start step 0");
                break;
            case 1:
                Debug.Log("Start step 1");
                break;
        }
    }

    /// <summary>
    /// This will drive the events in the first room of the tutorial
    /// </summary>
    void InitialRoom()
    {

    }
}

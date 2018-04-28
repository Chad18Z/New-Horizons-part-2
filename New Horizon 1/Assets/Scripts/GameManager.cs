using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for the main game level
/// </summary>
public class GameManager : AManager
{
    GameObject tutorialUI;
    Player player;

    protected override void Start()
    {
        // get reference to player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // get reference to the Sqdn Cdr UI and set it inactive
        tutorialUI = GameObject.FindGameObjectWithTag("tutorialUI");
        tutorialUI.SetActive(false); 

        EventManager.Instance.StartListening("NextStep", NextStep);
        InitialRoom();
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
                //Debug.Log("On step 0");
                break;
            case 1:
                //Debug.Log("On step 1");
                break;
        }
    }

    protected override void NextStep(EventParam e)
    {
        //Debug.Log("Going to next step");

        // Cleanup logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                //Debug.Log("Cleanup step 0");
                break;
            case 1:
                //Debug.Log("Cleanup step 1");
                break;
        }

        currStep++;

        // Start logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                //Debug.Log("Start step 0");
                break;
            case 1:
                //Debug.Log("Start step 1");
                break;
        }
    }

    /// <summary>
    /// This will drive the events in the first room of the tutorial
    /// </summary>
    void InitialRoom()
    {

        // set player controls in inactive
        player.PlayerCanInteract = false;

        // The first thing we need to do is make the camera black
        FadeManager.Instance.Fade(false, 2f);

        // Deliver first line from the Squadron Commander
        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1la");
        tutorialUI.SetActive(true);

        // 
    }
}

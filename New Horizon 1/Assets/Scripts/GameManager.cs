﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Manager for the main game level
/// </summary>
public class GameManager : AManager
{
    int enemyCount;
    int originalEnemyCount;

    [SerializeField]
    GameObject powerup;

    GameObject tutorialUI;
    Player player;
    bool timer = false;
    GameObject playerMessages;
    float fastBubbleRate = 100f;

    [SerializeField]
    GameObject dummyTCell;

    GameObject[] dummies = new GameObject[4];

    // array which holds the initial spawn points for first wave of dummy TCells
    GameObject[] firstSpawners;
    GameObject[] secondSpawners;

    SoundFile[] sound = new SoundFile[1];

    [SerializeField]
    GameObject cytoBlob;
    

    protected override void Start()
    {
        // number of enemies in the entire scene
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        originalEnemyCount = enemyCount;
      
        sound[0] = SoundFile.incomingRadio;

        // get reference to player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // first wave of dummy TCells
        firstSpawners = GameObject.FindGameObjectsWithTag("tCellSpawner1");

        // second wave of dummy TCells
        secondSpawners = GameObject.FindGameObjectsWithTag("tCellSpawner2");

        // get a reference to the text UI
        playerMessages = GameObject.FindGameObjectWithTag("messagesToPlayer");
        playerMessages.SetActive(false);

        // get reference to the Sqdn Cdr UI and set it inactive
        tutorialUI = GameObject.FindGameObjectWithTag("tutorialUI");
        tutorialUI.SetActive(false); 

        EventManager.Instance.StartListening("NextStep", NextStep);
        InitialRoom();
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;

        // Check enemycount for event triggers
        if (enemyCount == (originalEnemyCount - 3))
        {
            // enemies in room 5 have been killed by the player
            StartCoroutine(RoomFiveCleared());
        }
        else if (enemyCount == (originalEnemyCount - 6))
        {
            StartCoroutine(RoomSeven());
        }
        else if (enemyCount == (originalEnemyCount - 13))
        {
            StartCoroutine(Complete());
        }
    }

    IEnumerator Complete()
    {

        player.PlayerCanInteract = false;

        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1lh");
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(10);

        FadeManager.Instance.Fade(true, 2f);
        yield return new WaitForSeconds(5);

        MainMenu.GoToMenu();

    }

    protected override void Update()
    {
        //// For etsting purposes, this event can be called from anywhere
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EventManager.Instance.TriggerEvent("NextStep", null);
        //}

        // Update logic to run in this phase of the tutorial
        switch (currStep)
        {
            case 0:
                //Debug.Log("On step 0");
                break;
            case 1:
                //Debug.Log("On step 1");
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
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
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
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
                playerMessages.SetActive(false);
                break;
            case 2:
                ToSecondRoom();
                break;
            case 3:
                SecondRoom();
                break;
            case 4:
                ThirdRoom();
                break;
            case 5:
                FourthRoom();
                break;
            case 6:
                Cluster();
                break;
            case 7:
                FinalRoom();
                break;

        }
    }

    void FinalRoom()
    {
        StartCoroutine(FinalSequence());
    }
    void Cluster()
    {
        StartCoroutine(RoomSixCleared());
    }
    void FourthRoom()
    {
        StartCoroutine(FourthRoomSequence());
    }

    void ThirdRoom()
    {
        ClearDummyArray();
        StartCoroutine(ThirdRoomSequence());
    }

    IEnumerator FinalSequence()
    {
        playerMessages.GetComponentInChildren<Text>().text = "Protect the friendly cells!";
        playerMessages.SetActive(true);
        yield return new WaitForSeconds(2);

        playerMessages.SetActive(false);
        player.PlayerCanInteract = true;
    }
    IEnumerator RoomSixCleared()
    {
        player.PlayerCanInteract = false;

        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1lf");
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(3);


        playerMessages.GetComponentInChildren<Text>().text = "Destroy the cluster!";
        playerMessages.SetActive(true);
        player.PlayerCanInteract = true;

        yield return new WaitForSeconds(2);
        playerMessages.SetActive(false);

        
    }

    IEnumerator RoomSeven()
    {
        player.PlayerCanInteract = false;

        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1lg");
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(1);

        player.PlayerCanInteract = true;
    }

    IEnumerator RoomFiveCleared()
    {
        playerMessages.SetActive(false);

        player.PlayerCanInteract = false;
        yield return new WaitForSeconds(1);

        PlayIncomingRadio();

        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1le");
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(11);

        playerMessages.GetComponentInChildren<Text>().text = "Move to the powerup";
        playerMessages.SetActive(true);

        Vector2 powerUpSpawn = (Vector2)player.transform.position;
        powerUpSpawn.y += 7f;

        GameObject tempPowerUp = Instantiate(powerup);
        tempPowerUp.transform.position = powerUpSpawn;

        player.PlayerCanInteract = true;

    }

    IEnumerator FourthRoomSequence()
    {
        yield return new WaitForSeconds(1);

        playerMessages.GetComponentInChildren<Text>().text = "Press left mouse button to fire";
        playerMessages.SetActive(true);
        player.PlayerCanShoot = true;

    }

    IEnumerator ThirdRoomSequence()
    {
        player.PlayerCanInteract = false;
        yield return new WaitForSeconds(1);

        Vector2 cytoFireDirection = Vector2.left;
     
    
        GameObject cyto1 = Instantiate(cytoBlob, GameObject.FindGameObjectWithTag("shooterCell").transform.position, Quaternion.identity);
        Rigidbody2D cytoRb1 = cyto1.GetComponent<Rigidbody2D>();
        cytoRb1.AddForce(cytoFireDirection * 50, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.25f);

        GameObject cyto2 = Instantiate(cytoBlob, GameObject.FindGameObjectWithTag("shooterCell").transform.position, Quaternion.identity);
        Rigidbody2D cytoRb2 = cyto2.GetComponent<Rigidbody2D>();
        cytoRb2.AddForce(cytoFireDirection * 50, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.25f);

        GameObject cyto3 = Instantiate(cytoBlob, GameObject.FindGameObjectWithTag("shooterCell").transform.position, Quaternion.identity);
        Rigidbody2D cytoRb3 = cyto3.GetComponent<Rigidbody2D>();
        cytoRb3.AddForce(cytoFireDirection * 50, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);

        player.PlayerCanInteract = true;
    }
    void SecondRoom()
    {
        StartCoroutine(SecondRoomSequence());
    }

    public void TurnOffMessage()
    {
        playerMessages.SetActive(false);
    }


    /// <summary>
    /// This will drive the events in the first room of the tutorial
    /// </summary>
    void InitialRoom()
    {       

        // set player controls in inactive
        player.PlayerCanInteract = false;

        // begin timed sequence of events for the room
        StartCoroutine(FirstRoomSequence());

    }
    void ToSecondRoom()
    {
        // first thing, spawn the T-Cell fire team
        for (int i = 0; i < firstSpawners.Length; i++)
        {
            dummies[i] = Instantiate(dummyTCell);
            dummies[i].transform.position = firstSpawners[i].transform.position;
            dummies[i].GetComponent<dummyBubbles>().SetDestination = Vector3.up;
        }

    }
    IEnumerator SecondRoomSequence()
    {      
        yield return new WaitForSeconds(2);
        ClearDummyArray();

        GameObject tempCellCluster = GameObject.FindGameObjectWithTag("secondRoom");
        Vector3 tempPosition = tempCellCluster.transform.position;

        tempPosition.y = player.transform.position.y;
        tempCellCluster.transform.position = tempPosition;

        // enter the three other TCells
        // first thing, spawn the T-Cell fire team
        for (int i = 0; i < secondSpawners.Length; i++)
        {
            dummies[i] = Instantiate(dummyTCell);
            dummies[i].transform.position = secondSpawners[i].transform.position;
            dummies[i].transform.Rotate(0, 0, -90);
            dummies[i].GetComponent<dummyBubbles>().SetDestination = Vector3.right;
            dummies[i].GetComponent<dummyBubbles>().secondRoom = true;
        }

        player.PlayerCanInteract = false;

        yield return new WaitForSeconds(3);

        PlayIncomingRadio();

        yield return new WaitForSeconds(.5f);

        // Deliver first line from the Squadron Commander
        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1lc");
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(15);


        // Deliver first line from the Squadron Commander
        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1ld");
        tutorialUI.SetActive(true);

        yield return new WaitForSeconds(9);

        dummies[0].GetComponent<dummyBubbles>().SetDestination = Vector3.right;
        dummies[1].transform.Rotate(0, 0, 90);
        dummies[1].GetComponent<dummyBubbles>().SetDestination = Vector3.up;

        dummies[2].transform.Rotate(0, 0, -90);
        dummies[2].GetComponent<dummyBubbles>().SetDestination = Vector3.down;

        player.PlayerCanInteract = true;

    }
    IEnumerator FirstRoomSequence()
    {
        PlayIncomingRadio();

        // wait two seconds with black sceen
        yield return new WaitForSeconds(.5f);

        // Deliver first line from the Squadron Commander
        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1la");
        tutorialUI.SetActive(true);

        yield return new WaitForSeconds(3);

        // The first thing we need to do is make the camera black
        FadeManager.Instance.Fade(false, 2f);

        yield return new WaitForSeconds(2.5f);

        // Deliver first line from the Squadron Commander
        tutorialUI.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/s1lb");
        tutorialUI.SetActive(true);

        yield return new WaitForSeconds(5f);

        playerMessages.GetComponentInChildren<Text>().text = "Hold right mouse button to move";
        playerMessages.SetActive(true);
        player.PlayerCanInteract = true;
        player.PlayerCanShoot = false;
    }

    void ClearDummyArray()
    {
        for (int i = 0; i < dummies.Length; i++)
        {
            Destroy(dummies[i]);
        }           
    }

    void PlayIncomingRadio()
    {      
        SoundManager.Instance.DoPlayOneShot(sound, Camera.main.transform.position, .1f);
    }
     
}

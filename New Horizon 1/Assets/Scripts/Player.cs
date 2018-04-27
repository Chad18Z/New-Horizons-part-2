using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // If set to false, player can rotate but not move or fire
    public bool PlayerCanInteract = true;

    SoundFile[] singleShot;
    SoundFile[] chargeShot;

    Rigidbody2D rb2d;
    Vector3 normalScale;
    float chargeStartTime;
    float chargeTimeCurrent;
    float cytoSpeed = 40f; // speed at which cytoburst travels
    float reloadTime = 0.2f;
    float lastFireTime;
    float amountToInflate;
    int shotsToFire;
    bool fireReady;
    bool fireInvoked;
    GameObject cytoMountPoint; // spawnpoint for cytoblasts/blobs/beams/etc

    [SerializeField] float thrustMultiplier = 1f;
    [SerializeField] float maxChargeTime = 1.5f;
    [SerializeField] int maxBlobsPerShot = 10;
    [SerializeField] float shotSpread = 3f;
    [Range(0f, 1f)] [SerializeField] float maxInflation;
    [SerializeField] GameObject cytoBlobPrefab;
    
    //handles player power-ups 
    bool moveFast = false;
    bool unlimCyto = false;
    bool scoutBurst = false;
    bool isactive;
    float gotSpeed = 1f;
    float gotCyto = 1f;
    float gotBurst = 1f;

    // bubble jet support
    ParticleSystem bubbles;
    float idleStartSpeed = 3f;
    float idleRateOverTime = 7f;
    float maxSpeed = 15f;
    float maxRateOverTime = 115f;

    //Timer components 
    Timer powerupTimer;

	private UICellInfo playerInfo;

    // PROPERTIES
    #region
    /// <summary>
    /// sets/gets whether the player is moving fast
    /// </summary>
    /// <value>true if moving fast; otherwise, false</value>
    public bool MoveFast
    {
        get
        {
            return moveFast;
        }
        set
        {
            moveFast = value;
        }
    }

    /// <summary>
    /// gets whether the player has unlimited cytotoxin 
    /// </summary>
    /// <value>true if unlimited cytotoxin; otherwise, false</value>
    public bool UnlimCyto
    {
        get
        {
            return unlimCyto;
        }
        set
        {
            unlimCyto = value;
        }

    }

    /// <summary>
    /// gets whether the player has scout burst power up
    /// </summary>
    /// <value>true if scout burst activiated; otherwise, false</value>
    public bool Scouts
    {
        get
        {
            return scoutBurst;
        }
        set
        {
            scoutBurst = value;
        }
    }

    /// <summary>
    /// gets the timer for speed power up 
    /// </summary>
    /// <value>true if speed activiated; otherwise, false</value>
    public float SpeedTime
    {
        get { return gotSpeed; }
        set
        {
            gotSpeed = value;
            //if (MoveFast)
            //    powerupTimer.Run();
            //else
            //Debug.Log("Speed power-up has expired");
        }
    }

    /// <summary>
    /// gets the timer for unlimited cytotoxin power up 
    /// </summary>
    /// <value>true if unlimCyto activiated; otherwise, false</value>
    public float CytoTime
    {
        get { return gotCyto; }
        set
        {
            gotCyto = value;
            //if (UnlimCyto)
            //    powerupTimer.Run();
            //else
            //Debug.Log("UnlimCyto power-up has expired");
        }
    }

    /// <summary>
    /// gets the timer for scout burst power up 
    /// </summary>
    /// <value>true if scoutBurst activiated; otherwise, false</value>
    public float ScoutTime
    {
        get { return gotBurst; }
        set
        {
            gotBurst = value;
            //if (Scouts)
            //    powerupTimer.Run();
            //else
            //Debug.Log("Scout Burst power-up has expired");
        }
    }
    #endregion
    
    // Use this for initialization
    void Start()
    {
        singleShot = new SoundFile[1];
        singleShot[0] = SoundFile.singleShotSound;

        chargeShot = new SoundFile[1];
        chargeShot[0] = SoundFile.chargedShotSound;



        cytoMountPoint = GameObject.FindGameObjectWithTag("arrow");
        rb2d = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;
        lastFireTime = Time.time;

        //timer components
		powerupTimer = gameObject.AddComponent<Timer>();

		// 
		this.playerInfo = gameObject.AddComponent (typeof(UICellInfo)) as UICellInfo;

        // bubble jet
        bubbles = GameObject.FindGameObjectWithTag("bubbles").GetComponent<ParticleSystem>();
	}


    // Update is called once per frame
    void Update()
    {
        // i.e. not in a cutscene etc...
        if (PlayerCanInteract)
        {
            // Set the start time when the mouse is initially pressed down
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.Instance.DoPlayOneShot(chargeShot, Camera.main.transform.position, .1f);
                chargeStartTime = Time.time;
            }

            // While the mouse is being held down...
            if (Input.GetMouseButton(0))
            {
                // Set the charge amount to be how long in seconds the mouse was held down
                chargeTimeCurrent = Time.time - chargeStartTime;
                chargeTimeCurrent = Mathf.Clamp(chargeTimeCurrent, 0f, maxChargeTime);

                // Calculate how much to inflate based off the max amount allowed and the current charge time
                amountToInflate = 1 + (chargeTimeCurrent / maxChargeTime) * maxInflation;

                // Inflate that much
                transform.localScale = new Vector3(normalScale.x * amountToInflate, normalScale.y * amountToInflate, normalScale.z);
            }

            // When the fire button is released...
            if (Input.GetMouseButtonUp(0))
            {
                // ...do some boring math to figure out how many shots to fire
                float finalChargeTime = Time.time - chargeStartTime;
                finalChargeTime = Mathf.Clamp(finalChargeTime, 0, maxChargeTime);
                shotsToFire = (int)((finalChargeTime / maxChargeTime) * maxBlobsPerShot);
                if (shotsToFire == 0) shotsToFire = 1;

                // Flag to fire ASAP and go back to normal size
                fireInvoked = true;
                transform.localScale = normalScale;
            }

            // If it's flagged that you CAN fire and you SHOULD fire, fire
            if (fireReady && fireInvoked)
            {
                Fire(shotsToFire);
            }
        }

        // If you've had enough time to reload, flag that you're ready to fire
        if (!fireReady)
        {
            if (Time.time - lastFireTime >= reloadTime)
            {
                fireReady = true;
            }
        }
        
        if (Input.GetMouseButton(1) && PlayerCanInteract)
        {
            // Produce bubbles first
            var emmision = bubbles.emission;
            var main = bubbles.main;

            float rate = emmision.rateOverTime.constant;
            float speed = main.startSpeed.constant;

            // increase 
            rate += 10f;
            speed += 5f;

            // clamp
            if (rate > maxRateOverTime) rate = maxRateOverTime;
            if (speed > maxSpeed) speed = maxSpeed;

            // update values in the particle system
            emmision.rateOverTime = rate;
            main.startSpeed = speed;
            

            Vector3 directionToGo;

            // Get the difference between the mouse position and the player, times -1
            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionToGo = (mousePosition - (Vector2)transform.position).normalized;

            // Shoot the player in that direction, with the magnitude of the thrust multiplier times charge amount
            rb2d.AddForce(directionToGo * thrustMultiplier * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            // Produce bubbles first
            var emmision = bubbles.emission;
            var main = bubbles.main;

            float rate = emmision.rateOverTime.constant;
            float speed = main.startSpeed.constant;

            // increase 
            rate -= 11f;
            speed -= 6f;

            // clamp
            if (rate < idleRateOverTime) rate = idleRateOverTime;
            if (speed < idleStartSpeed) speed = idleStartSpeed;

            // update values in the particle system
            emmision.rateOverTime = rate;
            main.startSpeed = speed;

        }
    }

	// called if mouse is over any child object inheriting cell
	void OnMouseOver()
    {
		this.playerInfo.SetMouseOver (true);
		this.playerInfo.SetPlayerInfo (this);
	}

	// mouse leaves cell object
	void OnMouseExit() {
		this.playerInfo.SetMouseOver (false);
	}

    /// <summary>
    /// This method fires a cytoblob
    /// </summary>
    void Fire(int shotsToFire)
    {
        // Set the first bullet to have it's angle/velocity
        float shotOrder = (shotsToFire / 2f - shotsToFire) + 0.5f;

        // For every bullet to be fired...
        for (int i = 0; i < shotsToFire; i++)
        {
            // Do some boring math to figure out how to angle this shot
            float degreeRotation = shotOrder * shotSpread;

            //float speedMultiplier = shotsToFire / maxBlobsPerShot;
            //speedMultiplier = Mathf.Clamp(speedMultiplier, 0.5f, 1f);
            //float speedToFire = cytoSpeed * speedMultiplier;

            Vector2 randomVector = Random.insideUnitCircle.normalized * 0.1f;
            Vector2 cytoFireDirection = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
            cytoFireDirection = RotateVector2(cytoFireDirection, degreeRotation);
            cytoFireDirection += randomVector;
            GameObject cyto = Instantiate(cytoBlobPrefab, cytoMountPoint.transform.position, Quaternion.identity);
            Rigidbody2D cytoRb = cyto.GetComponent<Rigidbody2D>();
            cytoRb.AddForce(cytoFireDirection * cytoSpeed, ForceMode2D.Impulse);
            cytoRb.velocity = cytoRb.velocity + rb2d.velocity;
            
            shotOrder += 1f;
        }
        SoundManager.Instance.DoPlayOneShot(singleShot, Camera.main.transform.position, .2f);
    }

    Vector2 RotateVector2(Vector2 inputVector, float degrees)
    {
        Vector2 outputVector = inputVector;
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = inputVector.x;
        float ty = inputVector.y;
        outputVector.x = (cos * tx) - (sin * ty);
        outputVector.y = (sin * tx) + (cos * ty);

        lastFireTime = Time.time;
        fireReady = false;
        fireInvoked = false;

        return outputVector;
    }

	public Player getPlayer() {
		return this;
	}
}

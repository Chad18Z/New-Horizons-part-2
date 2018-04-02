using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float chargeStartTime;
    Rigidbody2D rb2d;
    Vector3 normalScale;
    float chargeTimeCurrent;
    GameObject guyImStuckTo;
    Vector3 guyImStuckToPositionDifference;
    bool stuckToEnemy;
    float timeAssaultStarted = 0f;

    [SerializeField] float thrustMultiplier = 1f;
    [SerializeField] float maxChargeTime = 1f;
    [SerializeField] GameObject missile;
    [SerializeField] float fireCooldownTime = .5f;
    [SerializeField] float totalAssaultTime = 4f;
    [Range(0f, 2f)] [SerializeField] float maxInflation;

    float cytoSpeed = 35f; // speed at which cytoburst travels

    [SerializeField]
    GameObject cytoBlobPrefab;

    GameObject cytoMountPoint; // spawnpoint for cytoblasts/blobs/beams/etc

  
    //handles player power-ups 
    bool moveFast = false;
    bool unlimCyto = false;
    bool scoutBurst = false;
    bool isactive;
    float gotSpeed = 1f;
    float gotCyto = 1f;
    float gotBurst = 1f;

    float amountToInflate;

    //Timer components 
    Timer powerupTimer;

	private UICellInfo playerInfo;

    // Use this for initialization
    void Start()
    {
        cytoMountPoint = GameObject.FindGameObjectWithTag("arrow");
        rb2d = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;

        //timer components
		powerupTimer = gameObject.AddComponent<Timer>();

		// 
		this.playerInfo = gameObject.AddComponent (typeof(UICellInfo)) as UICellInfo;
	}
    /// <summary>
    /// gets whether the player is moving fast
    /// </summary>
    /// <value>true if moving fast; otherwise, false</value>
    public bool MoveFast
    {
        get {
            moveFast = isactive;

            return isactive;
        }
        set {
            isactive = value;
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
            unlimCyto = isactive;
            return isactive;
        }
        set
        {
            isactive = value;
        }

    }

    /// <summary>
    /// gets whether the player has scout burst power up
    /// </summary>
    /// <value>true if scout burst activiated; otherwise, false</value>
    public bool Scouts
    {
        get {
            scoutBurst = isactive;
            return isactive;
        }
        set
        {
            isactive = value; 
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
            if (MoveFast)
                powerupTimer.Run();
            else
            Debug.Log("Speed power-up has expired");
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
            if (UnlimCyto)
                powerupTimer.Run();
            else
            Debug.Log("UnlimCyto power-up has expired");
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
            if (Scouts)
                powerupTimer.Run();
            else
            Debug.Log("Scout Burst power-up has expired");
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0)) { Fire(); }

        //// When the player releases the mouse button...
        if (Input.GetMouseButton(1))
        {
            Vector3 directionToGo;

            // Get the difference between the mouse position and the player, times -1
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionToGo = mousePosition - transform.position;

            // Shoot the player in that direction, with the magnitude of the thrust multiplier times charge amount
            rb2d.AddForce(directionToGo.normalized * thrustMultiplier * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

	// called if mouse is over any child object inheriting cell
	void OnMouseOver() {
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
    void Fire()
    {
        Vector2 cytoFireDirection = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)cytoMountPoint.transform.position).normalized;
        GameObject cyto = Instantiate(cytoBlobPrefab, cytoMountPoint.transform.position, Quaternion.identity);
        Rigidbody2D cytoRb = cyto.GetComponent<Rigidbody2D>();
        cytoRb.AddForce(cytoFireDirection * cytoSpeed, ForceMode2D.Impulse);
    }
}

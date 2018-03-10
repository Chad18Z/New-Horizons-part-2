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
    [Range(0f, 1f)] [SerializeField] float maxInflation;

    float cytoSpeed = 25f; // speed at which cytoburst travels

    [SerializeField]
    GameObject cytoBlobPrefab;

    GameObject cytoMountPoint; // spawnpoint for cytoblasts/blobs/beams/etc

  

    //handles player power-ups 
    bool moveFast = false;
    bool unlimCyto = false;
    bool scoutBurst = false;
    bool isactive;


    // Use this for initialization
    void Start()
    {
        cytoMountPoint = GameObject.FindGameObjectWithTag("arrow");
        rb2d = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;

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



    // Update is called once per frame
    void Update()
    {
        //float amountToInflate;

        //// When the player holds down the mouse...
        //if (Input.GetMouseButtonDown(0) && !stuckToEnemy)
        //{
        //    // ...record the time
        //    chargeStartTime = Time.time;
        //}

        //// While the mouse is being held down...
        //if (Input.GetMouseButton(1))
        //{
        //    // Set the charge amount to be how long in seconds the mouse was held down
        //    chargeTimeCurrent = Time.time - chargeStartTime;
        //    chargeTimeCurrent = Mathf.Clamp(chargeTimeCurrent, 0f, maxChargeTime);

        //    //// Calculate how much to inflate based off the max amount allowed and the current charge time
        //    //amountToInflate = 1 + (chargeTimeCurrent / maxChargeTime) * maxInflation;

        //    //// Inflate that much
        //    //transform.localScale = new Vector3(normalScale.x * amountToInflate, normalScale.y * amountToInflate, normalScale.z);
        //}

        //// When the player releases the mouse button...
        if (Input.GetMouseButton(1))
        {
            Vector3 directionToGo;

            // Get the difference between the mouse position and the player, times -1
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionToGo = mousePosition - transform.position;

            // Shoot the player in that direction, with the magnitude of the thrust multiplier times charge amount
            rb2d.AddForce(directionToGo.normalized * thrustMultiplier * Time.deltaTime, ForceMode2D.Impulse);

            transform.localScale = normalScale;
        }

        // When player fires 
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 cytoFireDirection = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)cytoMountPoint.transform.position).normalized;

            GameObject cyto = Instantiate(cytoBlobPrefab, cytoMountPoint.transform.position, Quaternion.identity);

            Rigidbody2D cytoRb = cyto.GetComponent<Rigidbody2D>();

            cytoRb.AddForce(cytoFireDirection * cytoSpeed, ForceMode2D.Impulse);

        }



        //if (stuckToEnemy)
        //{
        //    if (timeAssaultStarted == 0f)
        //    {
        //        timeAssaultStarted = Time.time;
        //        InvokeRepeating("FireMissile", .5f, fireCooldownTime);
        //    }
        //    if (Time.time - timeAssaultStarted > totalAssaultTime)
        //    {
        //        stuckToEnemy = false;
        //        timeAssaultStarted = 0f;
        //        CancelInvoke();
        //        rb2d.AddForce(guyImStuckToPositionDifference.normalized * 10, ForceMode2D.Impulse);
        //    }
        //}
    }

    //void LateUpdate()
    //{
    //    if (guyImStuckTo && stuckToEnemy)
    //    {
    //        transform.position = guyImStuckTo.transform.position + guyImStuckToPositionDifference;
    //    }
    //}

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject == guyImStuckTo)
    //    {
    //        stuckToEnemy = false;
    //    }
    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        guyImStuckTo = collision.gameObject;
    //        guyImStuckToPositionDifference = transform.position - guyImStuckTo.gameObject.transform.position;
    //        stuckToEnemy = true;
    //    }
    //}

    //void FireMissile()
    //{
    //    Vector3 positionDifference = guyImStuckTo.transform.position - transform.position;
    //    positionDifference.Normalize();

    //    float rot_z = Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;
    //    Quaternion missileRotation = Quaternion.Euler(0f, 0f, rot_z);

    //    GameObject createdMissile = Instantiate(missile, transform.position, missileRotation);
    //    createdMissile.GetComponent<SeekerMissile>().target = guyImStuckTo;
    //}

}

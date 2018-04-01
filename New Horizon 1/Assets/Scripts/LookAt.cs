using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    static Quaternion rot;
    RaycastHit2D hit; // reference to the collision point between raycast and other game objects

    //Get a reference to the cytospawnpoint, this will be the origin of our raycast
    GameObject cytoSpawn;

    //distance to raycast
    float dist = 100f;

	// Use this for initialization
	void Start () {

        cytoSpawn = GameObject.FindGameObjectWithTag("cytoMount"); // ref to cytospawnpoint
	}
    public static Quaternion GetRotation
    {
        get { return rot; }
    }
        
	
	//
	void Update ()
    {

        Vector3 diff = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot = Quaternion.Euler(0f, 0f, rot_z + 90);
        transform.rotation = rot;

        // check raycast to see if player is facing an enemy
        GameObject tempObject = CheckFacingEnemy(diff);
        if (tempObject != null)
        {
            DisplayEnemyInfo(tempObject);
        }
    }

    /// <summary>
    /// Draw ray, check for collision
    /// </summary>
    GameObject CheckFacingEnemy(Vector3 dir)
    {
        hit = Physics2D.Raycast(cytoSpawn.transform.position, -dir, dist);
        if (hit.collider == null || !hit.collider.gameObject.CompareTag("Enemy")) { return null; }
        else
        {
            return hit.collider.gameObject;
        }
    }

    /// <summary>
    /// Enemy display
    /// </summary>
    /// <param name="enemy"></param>
    void DisplayEnemyInfo(GameObject enemy)
    {

    }
}

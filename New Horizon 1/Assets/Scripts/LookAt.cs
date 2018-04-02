using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    static Quaternion rot;
    RaycastHit2D hit; // reference to the collision point between raycast and other game objects

    //Get a reference to the cytospawnpoint, this will be the origin of our raycast
    GameObject cytoSpawn;

    //distance to raycast
    float dist = 1000f;

    GameObject lightObject;
    ParticleSystem enemyHighlight;

    GameObject enemyHealth;

	// Use this for initialization
	void Start () {

        //Cursor.visible = false; // make the cursor invisible
        cytoSpawn = GameObject.FindGameObjectWithTag("cytoMount"); // ref to cytospawnpoint
        lightObject = GameObject.FindGameObjectWithTag("enemyHighlight");

        enemyHealth = GameObject.FindGameObjectWithTag("enemyCytoLevel");
        enemyHealth.SetActive(false);

        enemyHighlight = lightObject.GetComponentInChildren<ParticleSystem>();
        enemyHighlight.Play();
        enemyHighlight.enableEmission = false;
	}
    public static Quaternion GetRotation
    {
        get { return rot; }
    }
        	
	//
	void FixedUpdate ()
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
        if (hit.collider == null || !hit.collider.gameObject.CompareTag("Enemy"))
        {
            enemyHealth.SetActive(false);
            enemyHighlight.enableEmission = false;
            return null;
        }
        return hit.collider.gameObject;
    }

    /// <summary>
    /// Enemy display
    /// </summary>
    /// <param name="enemy"></param>
    void DisplayEnemyInfo(GameObject enemy)
    {
        lightObject.transform.rotation = enemy.transform.rotation;
        lightObject.transform.localScale = enemy.transform.localScale;
        lightObject.transform.position = enemy.transform.position;

        enemyHealth.transform.position = enemy.transform.position;
        enemyHealth.SetActive(true);
        enemyHighlight.enableEmission = true;
    }
}

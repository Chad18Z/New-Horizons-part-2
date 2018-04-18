using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Cell
{
    [SerializeField] RuntimeAnimatorController enemyAnimator1;
    [SerializeField] RuntimeAnimatorController enemyAnimator2;
    [SerializeField] RuntimeAnimatorController enemyAnimator3;

    [SerializeField]
    GameObject damageTextObject;

    [SerializeField]
    GameObject explosionParticle;



    float damageMultiplier = .5f; // multiplied times the magnitude of the velocity of collision with cytoblob

    // Use this for initialization
    protected override void Start()
    {
        int randomNum = Random.Range(1, 4);
        switch (randomNum)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator1;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator2;
                break;
            case 3:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator3;
                break;
            default:
                break;
        }

        base.Start();
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (health <= 0)
        {
            GameObject explosion = Instantiate(explosionParticle);
            explosion.transform.position = gameObject.transform.position;
            explosion.transform.localScale = gameObject.transform.localScale;
            Destroy(gameObject);
        }

    }
    /// <summary>
    /// Check for collision with cytoblob. This formula might need some refinement
    /// </summary>
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("cytoBlob"))
        {
            float tempHealth = (coll.relativeVelocity.magnitude * damageMultiplier) / gameObject.transform.localScale.x;           
            health -= tempHealth;
            int tHealth = (int)tempHealth;
            damageTextObject.GetComponentInChildren<Text>().text = tHealth.ToString();
            GameObject part = Instantiate(damageTextObject);
            part.transform.position = coll.transform.position;
         
        }
    }

}

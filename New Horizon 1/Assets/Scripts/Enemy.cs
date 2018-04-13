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
    GameObject damage;

    Text damageText;

    float damageMultiplier = .5f; // multiplied times the magnitude of the velocity of collision with cytoblob
    float tempHealth;

    // Use this for initialization
    protected override void Start()
    {
        damageText = GameObject.FindGameObjectWithTag("damage").GetComponent<Text>();
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


    }
    /// <summary>
    /// Check for collision with cytoblob. This formula might need some refinement
    /// </summary>
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("cytoBlob"))
        {
            tempHealth = (int)((coll.relativeVelocity.magnitude * damageMultiplier) / gameObject.transform.localScale.x);           
            health -= tempHealth;          
            GameObject part = Instantiate(damage);
            damage.transform.position = coll.transform.position;
            damageText.text = tempHealth.ToString();

        }
    }

}

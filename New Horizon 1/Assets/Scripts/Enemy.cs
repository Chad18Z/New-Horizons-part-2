using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Cell
{
    [SerializeField] RuntimeAnimatorController enemyAnimator1;
    [SerializeField] RuntimeAnimatorController enemyAnimator2;
    [SerializeField] RuntimeAnimatorController enemyAnimator3;

    float damageFromCyto = 10f;

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


    }
    /// <summary>
    /// Check for collision with cytoblob
    /// </summary>
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("cytoBlob"))
        {
            health -= damageFromCyto;
        }
    }

}

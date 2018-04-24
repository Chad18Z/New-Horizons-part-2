using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Cell
{
    [SerializeField] RuntimeAnimatorController enemyAnimator1;
    [SerializeField] RuntimeAnimatorController enemyAnimator2;
    [SerializeField] RuntimeAnimatorController enemyAnimator3;
    [SerializeField] Sprite deadEnemySprite1;
    [SerializeField] Sprite deadEnemySprite2;
    [SerializeField] Sprite deadEnemySprite3;
    [SerializeField] GameObject deadEnemy;

    Sprite selectedDeadSprite;
    Vector2 lastColliderImpactForce;

    [SerializeField] GameObject damageTextObject;

    [SerializeField] GameObject explosionParticle;

    SoundFile[] sounds;



    float damageMultiplier = 1f; // multiplied times the magnitude of the velocity of collision with cytoblob

    // Use this for initialization
    protected override void Start()
    {
        sounds = new SoundFile[1];
        sounds[0] = SoundFile.cancerExploding; 

        int randomNum = Random.Range(1, 4);
        switch (randomNum)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator1;
                selectedDeadSprite = deadEnemySprite1;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator2;
                selectedDeadSprite = deadEnemySprite2;
                break;
            case 3:
                GetComponent<Animator>().runtimeAnimatorController = enemyAnimator3;
                selectedDeadSprite = deadEnemySprite3;
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

            GameObject newDeadEnemy = Instantiate(deadEnemy, transform.position, Quaternion.identity);
            newDeadEnemy.GetComponent<SpriteRenderer>().sprite = selectedDeadSprite;
            newDeadEnemy.transform.localScale = transform.localScale * 0.25f;
            if (newDeadEnemy.transform.localScale.x < 0.3f) newDeadEnemy.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            newDeadEnemy.GetComponent<Rigidbody2D>().AddForce(lastColliderImpactForce * 0.0002f, ForceMode2D.Impulse);

            // play explosion sound
            SoundManager.Instance.DoPlayOneShot(sounds, Camera.main.transform.position, .4f);

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
            float tempHealth = (coll.relativeVelocity.magnitude * damageMultiplier); // gameObject.transform.localScale.x;           
            health -= tempHealth;
            int tHealth = (int)tempHealth;
            damageTextObject.GetComponentInChildren<Text>().text = tHealth.ToString();

            //GameObject part = Instantiate(damageTextObject);
            //part.transform.position = coll.transform.position;

            lastColliderImpactForce = coll.relativeVelocity;
        }
    }

}

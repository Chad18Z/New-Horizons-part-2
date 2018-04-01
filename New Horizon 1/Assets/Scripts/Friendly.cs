using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Cell
{
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;

    [SerializeField] private float wanderForce;

    private GameObject decayParticlePrefab;

    private float currWanderInterval = 0f;
    private Vector2 currVelocity;
    private float timer = 0f;

    private SpriteRenderer spriteRenderer;
    private Color deadColor = new Color(0.37f, 0f, 0.77f); // Dark purple color
    private float losingHealthTimer = 0f;
    [SerializeField] private float decayParticleInterval = 0.75f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        decayParticlePrefab = Resources.Load<GameObject>("DecayParticle");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Wander();
    }

    /// <summary>
    /// Applies a force in the current wander direction, which changes every interval (random range between min and max WanderTime)
    /// </summary>
    private void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= currWanderInterval)
        {
            currWanderInterval = Random.Range(minWanderTime, maxWanderTime);
            currVelocity = Random.insideUnitCircle * wanderForce;
            rigidBody.AddForce(currVelocity);
            timer = 0f;
        }
        else
        {
            rigidBody.AddForce(currVelocity);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 0.025f;
            losingHealthTimer += Time.deltaTime;
            if (losingHealthTimer >= decayParticleInterval)
            {
                losingHealthTimer = 0f;
                GameObject a = Instantiate(decayParticlePrefab, GetRandomPositionWithinCollider(), Quaternion.identity);
                //Debug.Log(a);
            }
        }
        UpdateSprite();
    }

    /// <summary>
    /// Changes the sprite based on current health or other events
    /// </summary>
    private void UpdateSprite()
    {
        float t = 1 - health / maxHealth;
        spriteRenderer.color = Color.Lerp(Color.white, deadColor, t);
    }
}

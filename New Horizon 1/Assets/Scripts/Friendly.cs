using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Cell
{
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;

    [SerializeField] private float wanderForce;

    private float currWanderInterval = 0f;
    private Vector2 currVelocity;
    private float timer = 0f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
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
}

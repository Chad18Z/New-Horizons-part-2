using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    [SerializeField] protected float massMultiplier = 25;

    protected Rigidbody2D rigidBody;

    // Use this for initialization
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = massMultiplier * (Mathf.PI * Mathf.Pow((transform.localScale.x / 2), 2));
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}

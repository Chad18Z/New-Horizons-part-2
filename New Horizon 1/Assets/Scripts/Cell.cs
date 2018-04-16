using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] protected float health;
    protected float maxHealth;
    public float Health { get { return health/maxHealth; } }

 

    [SerializeField] protected float massMultiplier = 25;

    protected Rigidbody2D rigidBody;

	protected UICellInfo cellInfo;

    // Use this for initialization
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = massMultiplier * (Mathf.PI * Mathf.Pow((transform.localScale.x / 2), 2));
        maxHealth = health;

        this.cellInfo = gameObject.AddComponent (typeof(UICellInfo)) as UICellInfo;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected Vector2 GetRandomPositionWithinCollider()
    {
        Vector2 center = transform.position;
        center += Random.insideUnitCircle * GetComponent<CircleCollider2D>().radius;
        return center;
    }

	// called if mouse is over any child object inheriting cell
	void OnMouseOver() {
		this.cellInfo.SetMouseOver (true);
		this.cellInfo.SetCellInfo (this);
	}

	// mouse leaves cell object
	void OnMouseExit() {
		this.cellInfo.SetMouseOver (false);
	}

}

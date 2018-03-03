using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    [SerializeField] protected float massMultiplier = 25;

    protected Rigidbody2D rigidBody;

	private UICellInfo cellInfo;

    // Use this for initialization
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = massMultiplier * (Mathf.PI * Mathf.Pow((transform.localScale.x / 2), 2));

		this.cellInfo = gameObject.AddComponent (typeof(UICellInfo)) as UICellInfo;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

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

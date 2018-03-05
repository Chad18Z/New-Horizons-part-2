using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cytoblast : MonoBehaviour {


    Rigidbody2D rb;
    Vector2 dir = Vector2.zero;
    float speed = 10f;

   public Vector2 SetDirection
    {
        set { dir = value; }
    }

    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.parent = null;
        
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

		if (dir != Vector2.zero)
        {
            rb.AddForce(dir * speed, ForceMode2D.Impulse);
            dir = Vector2.zero;
        }
	}
}

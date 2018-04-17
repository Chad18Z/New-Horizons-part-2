using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    Rigidbody2D rb;
    float speed = 5.0f;
    float fadeSpeed = .6f;
    Text text;
    Color color;
    
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();

        text = gameObject.GetComponentInChildren<Text>();
        color = text.color;
        Vector2 randomVector = Random.insideUnitCircle.normalized;
        speed = Random.Range(5.0f, 6.0f);
        rb.AddForce(randomVector * speed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        color.a -= fadeSpeed * Time.deltaTime;
        text.color = color;

        if (color.a <= 0) Destroy(gameObject);
	}
}

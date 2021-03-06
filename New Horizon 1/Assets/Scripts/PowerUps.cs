﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    

    //sprite choices
    [SerializeField]
    Sprite pillSprite0;
    [SerializeField]
    Sprite pillSprite1;
    [SerializeField]
    Sprite pillSprite2;

    Player player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// detects collision with player and turns on specific power up
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
       //detect which sprite player is colliding into and change bools and floats in player
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite0)
        {
            Destroy(gameObject);
            player.MoveFast = true;
            player.SpeedTime = 5f;           
        }
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite1)
        {
            Destroy(gameObject);
            player.UnlimCyto = true;
            player.CytoTime = 5f;       
        }
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite2)
        {
            Destroy(gameObject);
            player.Scouts = true;
            player.ScoutTime = 5f;
        }
    }

    /// <summary>
    /// chooses sprite for power up on creation
    /// </summary>
    void SpawnPowerUp()
    {
        
        // select sprite
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber == 0)
        {
            spriteRenderer.sprite = pillSprite0;
        }
        else if (spriteNumber == 1)
        {
            spriteRenderer.sprite = pillSprite1;
        }
        else
        {
            spriteRenderer.sprite = pillSprite2;
        }
    }
}

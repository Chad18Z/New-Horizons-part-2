using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    // creating power ups
    [SerializeField]
    GameObject prefabPowerUps;

    //sprite choices
    [SerializeField]
    Sprite pillSprite0;
    [SerializeField]
    Sprite pillSprite1;
    [SerializeField]
    Sprite pillSprite2;

	// Use this for initialization
	void Start () {
		
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
        Player variableChanger = GetComponent<Player>();
        Timer powerUpDuration = GetComponent<Timer>();
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite0)
        {
            Destroy(gameObject);
            variableChanger.MoveFast = true;
            powerUpDuration.Duration = 5;
            powerUpDuration.Run();
        }
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite1)
        {
            Destroy(gameObject);
            variableChanger.UnlimCyto = true;
            powerUpDuration.Duration = 5;
            powerUpDuration.Run();
        }
        if (collision.gameObject.tag == "Player" && spriteRenderer == pillSprite2)
        {
            Destroy(gameObject);
            variableChanger.Scouts = true;
            powerUpDuration.Duration = 5;
            powerUpDuration.Run();
        }
    }

    /// <summary>
    /// chooses sprite for power up on creation
    /// </summary>
    void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(prefabPowerUps) as GameObject;
        
        // select sprite
        SpriteRenderer spriteRenderer = powerUp.GetComponent<SpriteRenderer>();
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

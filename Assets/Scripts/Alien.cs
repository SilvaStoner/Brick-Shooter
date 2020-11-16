using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {
    
    public float moveAmount = 15f;//how far the alien can move
    public float moveSpeed = 2f;//alien move speed
    public GameObject laser;//alien laser object
    public float laserSpeed;//how fast the alien laser moves
    
    private int direction= 1;//alien move direction
    private Rigidbody2D rigidbody;//the rigid body component
    private float timer;//time to wait until the alien can shoot another laser

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = Random.Range(1f, 5f);//chose a random number
    }

    // Update is called once per frame
    void Update () {
        //count down to zero
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            //shoot the laser towards the player
            var laserClone = Instantiate(laser, new Vector3(transform.position.x, transform.position.y - .5f, 0), Quaternion.identity);
            laserClone.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * laserSpeed * Time.deltaTime);
            timer = Random.Range(1f, 5f);//reset the time to a random value
        }

        //Move the alien based on the direction
        switch (direction)
        {
            //Move the alien left
            case -1:
                if (transform.position.x > -moveAmount)
                {
                    rigidbody.velocity = new Vector2(-transform.right.x * moveSpeed, rigidbody.velocity.y);
                }
                else
                    direction = 1;
                break;

            //Move the alien right
            case 1:
                if (transform.position.x < moveAmount)
                {
                    rigidbody.velocity = new Vector2(transform.right.x * moveSpeed, rigidbody.velocity.y);
                }
                else
                    direction = -1;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player laser hit the alien
        if (collision.CompareTag("laser"))
        {
            Destroy(gameObject);//destory the alien
            GameManager.INSTNANCE.AddScoreAlien(2);//add score
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(collision.gameObject);
        }

        //if ball hit the alien
        if (collision.CompareTag("Ball"))
        {
            Destroy(gameObject);//destory the alien
            GameManager.INSTNANCE.AddScoreAlien(2);//add score
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rigidBoady;//RigidBody component attached to the ball
    private bool isBallMoving = false;//Check if the ball is moving
    private AudioSource audioSource;

    public float force = 5f;// the amount of force to add to the ball movement
    public Transform ballSpawn;//The positon where the ball respawns
    public AudioClip paddleSound;

	// Use this for initialization
	void Start () {
        rigidBoady = GetComponent<Rigidbody2D>();// Get the rigidBody component attach to the ball
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //Add force to the ball if the right mouse button is clicked and the ball is not moving
		if(Input.GetKey(KeyCode.Mouse1) && !isBallMoving)
        {
            isBallMoving = true;//Set game started to true to prevent adding force every time the right mouse button is clicked
            transform.SetParent(null);//Remove the parent child relationship from the ball
            rigidBoady.isKinematic = false;//Set the rigid body is kenematic property to false
            rigidBoady.AddForce(Vector2.up * force);//Add force to the ball to move it around
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //play sound if it hits the paddle
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(paddleSound);//play sound when the ball hit the paddle
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the ball hit the bottom wall
        if (collision.CompareTag("bottomWall"))
        {
            rigidBoady.velocity = Vector2.zero;//set the ball velocit to 0 to stop it moving
            isBallMoving = false;//set is ball moving to false
            transform.parent = ballSpawn;//Set the parent of the ball to the player
            transform.position = ballSpawn.position;// set the ball position to the player postion
            GameManager.INSTNANCE.LoseLife();//Lose 1 life everytime the ball hit the bottom wall
        }
    }
}

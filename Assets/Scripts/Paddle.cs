using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    public float speed = 5.0f; //Paddle movment speed
    public float movementBound;//how far the paddle can move left or right
    public GameObject laser;//player laser object
    public AudioClip laserSound;//lasr shot sound

    private bool canShoot = false;//can the player shoot laser or not
    private Menu menu;//menu object
    private float laserTimer = 15f;//how long the player laser last
    private AudioSource audioSource;// audio source

	// Use this for initialization
	void Start () {
        menu = new Menu();//initialise the menu object
        audioSource = GetComponent<AudioSource>();//initialise the audio source
	}
	
	// Update is called once per frame
	void Update () {

        float translate = Input.GetAxis("Mouse X") * speed;//Get the mouse X axis multiply by the movement speed
        translate *= Time.deltaTime;//Move the paddle every second
        transform.Translate(translate, 0, 0);//Move the paddle on the X axis

        //Prevent the paddle from moving out of the left bounds of the camera
        if (transform.position.x < -movementBound)
            transform.position = new Vector2(-movementBound, transform.position.y);

        //Prevent the paddle from moving out of the right bounds of the camera
        if (transform.position.x > movementBound)
            transform.position = new Vector2(movementBound, transform.position.y);

        //Check if the player can shoot and the game is not paused then shoot laser
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && !menu.IsPaused)
        {
            var laserClone = Instantiate(laser, new Vector3(transform.position.x, transform.position.y + .5f, 0), Quaternion.identity);
            laserClone.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f * Time.deltaTime);

            audioSource.PlayOneShot(laserSound);//play sound when laser is shot
            audioSource.pitch = 1.5f;
        }

        //Start a count down timer
        if(canShoot)
            laserTimer -= Time.deltaTime;

        //when it reaches zero you cannot shoot lasers
        //until you get a new laser power up
        if (Mathf.Round(laserTimer) <= 0)
        {
            canShoot = false;
            laserTimer = 15f;//reset the time back to 15 seconds
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the object that hit the paddle is tag with death or enemy laser
        if (collision.CompareTag("death") || collision.CompareTag("enemy_laser"))
        {
            GameManager.INSTNANCE.LoseLife();//if is the death object then reduce the lives by 1
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//stop the object from moving
            Destroy(collision.gameObject);//destroy the object
        }

        //Get the laser power up to allow the player to shoot
        if (collision.CompareTag("powerup_laser"))
        {
            canShoot = true;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//stop the object from moving
            Destroy(collision.gameObject);//destroy the laser power up
        }

        //Power up to add live
        if (collision.CompareTag("fastbrick"))
        {
            GameManager.INSTNANCE.AddLife();//Add life if is not already full (3)
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//stop the object from moving
            Destroy(collision.gameObject);//destroy the laser power up
        }

    }
}

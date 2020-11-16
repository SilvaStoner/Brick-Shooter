using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public GameObject breakEffect;//break effect
    public GameObject laserPowerUp;
    public GameObject livesPowerUp;
    public GameObject deathPowerUp;
    public AudioClip breakSound;

    public int lives;//brick life

    private int which;//Which powerup to spawn
    private AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the ball is hitting the bricks
        if (collision.collider.CompareTag("Ball"))
        {
            which = Random.Range(0, 10);//random number from 0 - 9

            //play sound if the ball hits the brick
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(breakSound);

            lives--;//subract one life if the ball hit the brick
            Instantiate(breakEffect, transform.position, Quaternion.identity);//Create a particle effect to simulate breaking effect
            
            if(lives < 1)
            {
                Destroy(gameObject);//Destroy the brick the brick if life is 0
                GameManager.INSTNANCE.AddScore();//Add score everytime the ball hit a brick

                /***
                 * if which is 0 spawn laser power up
                 * if is 1 spawn lives power up
                 * if is 2 spawn death power up
                 * */
                switch (which)
                {
                    case 0:
                        Instantiate(laserPowerUp, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(livesPowerUp, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(deathPowerUp, transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
                    
            } 
        }
    }
}

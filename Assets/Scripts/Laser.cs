using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public LaserOwner laserOwner;//is the laser player laser or alien

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy the laser gameobject if it touches the top boundary
        if (collision.CompareTag("topWall") || collision.CompareTag("bottomWall"))
        {
            Debug.Log("Bullet: Hit Top Wall");
            Destroy(gameObject);
        }

        //Destroy brick if the laser hits it and the laser is shot by the player
        if (collision.CompareTag("brick") && laserOwner == LaserOwner.Player)
        {
            Debug.Log("Bullet: Hit a Brick");
            Destroy(collision.gameObject);//destroy brick
            GameManager.INSTNANCE.AddScore();//add score
            Destroy(gameObject);//destroy the laser
        }
    }

    //Who the laser object belongs to
    public enum LaserOwner
    {
        Player,
        Alien
    }
}
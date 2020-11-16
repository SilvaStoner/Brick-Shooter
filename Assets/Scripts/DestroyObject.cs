using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy gameobject if it hits the bottom wall
        if (collision.CompareTag("bottomWall"))
        {
            Destroy(gameObject);
        }
    }
}

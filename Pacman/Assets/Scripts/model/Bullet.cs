using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public float speed = 20f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Maze")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Blinky")
        {
            GhostMove.blinkyHp -= 20;
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Inky")
        {
            GhostMove.inkyHp -= 20;
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Pinky")
        {
            GhostMove.pinkyHp -= 20;
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "Clyde")
        {
            GhostMove.clydeHp -= 20;
            Destroy(gameObject);
        }
    }
}

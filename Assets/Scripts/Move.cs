﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    private float x_velocity;       // speed of object in the x direction
    private float y_velocity;       // speed of object in the y direction

    void Start()
    {

        x_velocity = Random.Range(-0.10f, 0.10f);
        y_velocity = Random.Range(-0.10f, 0.10f);
        
    }

    // Moves on the screen the object based on the x_velocity and y_velocity
    void Update()
    {   
        //transform.position = new Vector3(transform.position.x + x_velocity, transform.position.y + y_velocity, transform.position.z);
    }

    // If we hit the left or right boundary, invert x direction.
    // If we hit the top or bottom boundary, invert y direction.
    void OnCollisionEnter2D(Collision2D collision)
    {
        var name = collision.gameObject.name;
        var tag = collision.gameObject.tag;
        
        if (name == "RightCollider" || name == "LeftCollider")
        {
            x_velocity *= -1;
        }
        if (name == "TopCollider" || name == "BottomCollider")
        {
            y_velocity *= -1;
        }
        if (tag == "Match" || tag == "NotMatch")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(50f * new Vector2(Input.GetAxis("Horizontal")*-1, Input.GetAxis("Vertical")*-1));
            x_velocity = x_velocity + (-1*(Input.GetAxis("Horizontal")));
            y_velocity = y_velocity + (-1*(Input.GetAxis("Vertical")));
            Debug.Log("collision with " + collision.gameObject.name);
        }
    }
}

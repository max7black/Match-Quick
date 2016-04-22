using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour {

  //  private float[] velocities = { -0.08f, 0.08f };   // array of possible intial x or y velocities
    private float x_velocity;       // speed of object in the x direction
    private float y_velocity;       // speed of object in the y direction

    void Start()
    {
        x_velocity = Random.Range(-0.10f, 0.10f);
        y_velocity = Random.Range(-0.10f, 0.10f);
 //       x_velocity = velocities[Random.Range(0, velocities.Length)];
 //       y_velocity = velocities[Random.Range(0, velocities.Length)];
    }

    // Moves on the screen the object based on the x_velocity and y_velocity
    void Update()
    {
        transform.position = new Vector3(transform.position.x + x_velocity, transform.position.y + y_velocity, transform.position.z);
        
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
            Debug.Log("Right/left colllision");

        }
        if (name == "TopCollider" || name == "BottomCollider")
        {
            y_velocity *= -1;
            Debug.Log("top/bottom colllision");
        }
        /*
        if (tag == "Match" || tag == "NotMatch")
        {
            var orthogonalVector = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            var collisionAngle = Vector2.Angle(orthogonalVector, new Vector2(x_velocity, y_velocity));

            x_velocity *= Mathf.Sin(collisionAngle);
            y_velocity *= Mathf.Cos(collisionAngle);

         //   Debug.Log("collision with " + collision.gameObject.name);
        }
        */
    }
}

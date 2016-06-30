using UnityEngine;
using System.Collections;


public class Move2 : MonoBehaviour
{

    //  private float[] velocities = { -0.08f, 0.08f };   // array of possible intial x or y velocities
    private float x_velocity;       // speed of object in the x direction
    private float y_velocity;       // speed of object in the y direction
    private int index;
    public Vector3 cameraPos;
    public AudioSource bounceSound;

    void Start()
    {
        x_velocity = Random.Range(-0.10f, 0.10f);
        y_velocity = Random.Range(-0.10f, 0.10f);

        for (int i = 0; i < CreateObject2.numberOfObjects; i++)
        {
            if (name == CreateObject2.matchObjects[i].name)
            {
                index = CreateObject2.matchObjects.IndexOf(gameObject);
            }
        }
        cameraPos = MatchManager2.cameraPos;
    }

    // Moves on the screen the object based on the x_velocity and y_velocity
    void Update()
    {
        transform.position = new Vector3(transform.position.x + x_velocity, transform.position.y + y_velocity, transform.position.z);

        if (CreateObject2.inGoal[index] == true)
        {
            transform.position = new Vector3(cameraPos.x, cameraPos.y, transform.position.z);
            CreateObject2.inGoal[index] = false;
        }

    }

    // If we hit the left or right boundary, invert x direction.
    // If we hit the top or bottom boundary, invert y direction.
    void OnCollisionEnter2D(Collision2D collision)
    {
        var name = collision.gameObject.name;

        bounceSound = collision.gameObject.GetComponent<AudioSource>();

        if (name == "RightCollider" || name == "LeftCollider")
        {
            x_velocity *= -1;
            bounceSound.PlayOneShot(Resources.Load("Sounds/bounce_sound") as AudioClip, SoundManager.hSliderValue);
        }
        if (name == "TopCollider" || name == "BottomCollider")
        {
            y_velocity *= -1;
            bounceSound.PlayOneShot(Resources.Load("Sounds/bounce_sound") as AudioClip, SoundManager.hSliderValue);
        }
    }
}

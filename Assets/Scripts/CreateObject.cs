using UnityEngine;
using System.Collections;

public class CreateObject : MonoBehaviour
{
    public Sprite sprite;          // variable for the sprite we want

    void Awake()
    {
        sprite = Resources.Load<Sprite>("Sprites/RedOval");

    }

    void Start()
    {

        // initalize the object 
        GameObject matchObject = new GameObject("Object 1");

        // Assign the sprite we want to the object
        matchObject.AddComponent<SpriteRenderer>();
        matchObject.GetComponent<SpriteRenderer>().sprite = sprite;

        // Change the spawn loaction to be random
        matchObject.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), Random.Range(-4.0f, 4.0f), 0);

        // Add the Collider to our object
        matchObject.AddComponent<PolygonCollider2D>();

        // Add the Rigidbody2D to our object so it's affected by physics
        matchObject.AddComponent<Rigidbody2D>();
        matchObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        // Add the Move script to our object
        matchObject.AddComponent<Move>();

        // Add the ClickAndDrag script to our object
        matchObject.AddComponent<ClickAndDrag>();


    }
}

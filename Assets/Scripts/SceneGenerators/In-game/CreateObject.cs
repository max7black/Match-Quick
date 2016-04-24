using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class CreateObject : MonoBehaviour
{
    public static int numberOfObjects = 5;          // number of the objects we want to create to be matched
    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    public Vector3 cameraPos;
    private float scale;
    private int theMatchIndex;
    public static List<Sprite> sprite = new List<Sprite>();                                                // variable for the sprite we want
    public static List<GameObject> matchObjects = new List<GameObject>();  // variable for the object we are creating
    public static GameObject theMatch;
    private string[] spriteLocations = { "Sprites/Squares/Red-square", "Sprites/Squares/Blue-square",       // These are the locations of our sprite images
        "Sprites/Squares/Green-square", "Sprites/Squares/Yellow-square", "Sprites/Squares/Orange-square"};

    void Awake()
    {
        // load the sprite we want in our sprite variable
        for (int i = 0; i < numberOfObjects; i++)
        {
            sprite.Add(new Sprite());
            sprite[i] = Resources.Load<Sprite>(spriteLocations[i]);
        }
        // Set theMatchIndex equal to random nubmer between 0 and the number of objects
        theMatchIndex = Random.Range(0, numberOfObjects);
    }

    void Start()
    {
        // Get the screen size and camera position 
        screenSize = gameObject.GetComponent<Boundary>().screenSize;        
        cameraPos = gameObject.GetComponent<Boundary>().cameraPos;
        scale = screenSize.x / screenSize.y;

        // initalize the object 
        for (int i = 0; i < numberOfObjects; i++)
        {
            matchObjects.Add(new GameObject());
            // Name our objects
            matchObjects[i].name = "Object " + i;
            

            // Assign the sprite we want to the object
            matchObjects[i].AddComponent<SpriteRenderer>();
            matchObjects[i].GetComponent<SpriteRenderer>().sprite = sprite[i];


            // Change the spawn location to be random in the camera view      
            matchObjects[i].transform.position = new Vector3(Random.Range(cameraPos.x-(3*screenSize.x/4), cameraPos.x+(3*screenSize.x/4)), 
                Random.Range(cameraPos.y-(3*screenSize.y/4),cameraPos.y), 0);
      

            // Change the local scale of object to be approriately sized for the screen
            matchObjects[i].transform.localScale = new Vector3(scale/numberOfObjects, scale/numberOfObjects, colDepth);

            // Add the Collider to our object
            matchObjects[i].AddComponent<PolygonCollider2D>();

            // Add the Rigidbody2D to our object so it's affected by physics
            matchObjects[i].AddComponent<Rigidbody2D>();
            matchObjects[i].GetComponent<Rigidbody2D>().gravityScale = 0;

            // Add the Move script to our object
            matchObjects[i].AddComponent<Move>();

            // Add the ClickAndDrag script to our object
            matchObjects[i].AddComponent<ClickAndDrag>();
            if (i != theMatchIndex)
            {
                matchObjects[i].tag = "NotMatch";
            }
            
        }

        // Choose a random object to be the Match
        matchObjects[theMatchIndex].tag = "Match";

        // Make a copy of the matchObjects sprite, at index TheMatchIndex, and store it in theMatch
        theMatch = new GameObject();
        theMatch.name = "theMatch";
        theMatch.AddComponent<SpriteRenderer>();
        theMatch.GetComponent<SpriteRenderer>().sprite = sprite[theMatchIndex];                             // put the correct sprite on the match
        theMatch.transform.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y - (Boundary.topCollider.transform.position.y), 0);      // put theMatch in the middle of the goal




    }

}

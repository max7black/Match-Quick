using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class CreateObject2 : MonoBehaviour
{
    public static int numberOfObjects = 5;          // number of the objects we want to create to be matched
    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    public Vector3 cameraPos;
    private float scale;
    public new AudioSource audio;
    private static int theMatchIndex;
    public static List<Sprite> sprite = new List<Sprite>();                                                // variable for the sprite we want
    public static List<GameObject> matchObjects = new List<GameObject>();  // variable for the object we are creating
    public static List<bool> inGoal = new List<bool>();
    public static List<GameObject> theMatch = new List<GameObject>();
    public static List<string> spriteLocations = new List<string> { "Sprites/Squares/Red-square", "Sprites/Squares/Blue-square",       // These are the locations of our sprite images
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
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        scale = screenSize.x / screenSize.y;

        // initalize the object 
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Set all of the inGoal lists to bool
            inGoal.Add(new bool());
            inGoal[i] = false;

            matchObjects.Add(new GameObject());
            // Name our objects
            matchObjects[i].name = "Object " + i;


            // Assign the sprite we want to the object
            matchObjects[i].AddComponent<SpriteRenderer>();
            matchObjects[i].GetComponent<SpriteRenderer>().sprite = sprite[i];


            // Change the spawn location to be random in the camera view      
            matchObjects[i].transform.position = new Vector3(Random.Range(cameraPos.x - (3 * screenSize.x / 4), cameraPos.x + (3 * screenSize.x / 4)),
                Random.Range(cameraPos.y - (3 * screenSize.y / 4), cameraPos.y), 0);


            // Change the local scale of object to be approriately sized for the screen
            matchObjects[i].transform.localScale = new Vector3(scale/numberOfObjects, scale/numberOfObjects, colDepth);

            // Add the Collider to our object
            matchObjects[i].AddComponent<PolygonCollider2D>();

            // Add the Rigidbody2D to our object so it's affected by physics
            matchObjects[i].AddComponent<Rigidbody2D>();
            matchObjects[i].GetComponent<Rigidbody2D>().gravityScale = 0;

            // Add the Move2 script to our object
            matchObjects[i].AddComponent<Move2>();

            // Add the ClickAndDrag script to our object
            matchObjects[i].AddComponent<ClickAndDrag>();
            if (i != theMatchIndex)
            {
                matchObjects[i].tag = "NotMatch";
            }
        }
        // Choose a random object to be the Match
        matchObjects[theMatchIndex].tag = "Match";

        // Display all the objects that aren't the match at the top of the screen
        for (int i = 0; i < numberOfObjects; i++)
        {            
            theMatch.Add(new GameObject());
            theMatch[i].name = "Display object" + i;
            theMatch[i].transform.position = new Vector3(-screenSize.x / 2 + (i * scale), cameraPos.y + screenSize.y - ((cameraPos.y + screenSize.y - colDepth / 4) - colDepth / 2), 0);      // put theMatch
            theMatch[i].transform.localScale = new Vector3(scale / numberOfObjects, scale / numberOfObjects, colDepth);
            theMatch[i].AddComponent<SpriteRenderer>();
            theMatch[i].GetComponent<SpriteRenderer>().sprite = sprite[i];      // put the correct sprite on the match
            if(matchObjects[i].tag == "Match")
            {
                theMatch[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            audio = theMatch[i].AddComponent<AudioSource>();
            audio.clip = Resources.Load("Sounds/match_ding") as AudioClip;
            audio.volume = audio.volume * SoundManager.hSliderValue;
        }        
    }
}

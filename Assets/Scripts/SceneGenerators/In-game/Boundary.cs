using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    public Vector3 cameraPos;
    public static GameObject topCollider;
    public Transform bottomCollider;
    public Transform leftCollider;
    public Transform rightCollider;
    public GameObject goal;
    private Sprite spriteGoal;               // variable for the sprite we want for the goal
                                         

    void Awake()
    {
        // load the sprite we want in our sprite variable
        spriteGoal = Resources.Load<Sprite>("Sprites/Goal");

    }

    void Start()
    {
        //Generate our empty objects
        topCollider = new GameObject();
        bottomCollider = new GameObject().transform;
        rightCollider = new GameObject().transform;
        leftCollider = new GameObject().transform;
        goal = new GameObject();

        // Add our goal sprite to the goal object
        topCollider.AddComponent<SpriteRenderer>();
        topCollider.GetComponent<SpriteRenderer>().sprite = spriteGoal;

        //Name our objects 
        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        rightCollider.name = "RightCollider";
        leftCollider.name = "LeftCollider";
        goal.name = "Goal";

        //Add the colliders
        topCollider.gameObject.AddComponent<BoxCollider2D>();
        bottomCollider.gameObject.AddComponent<BoxCollider2D>();
        rightCollider.gameObject.AddComponent<BoxCollider2D>();
        leftCollider.gameObject.AddComponent<BoxCollider2D>();
        goal.gameObject.AddComponent<BoxCollider2D>();

        //Make them the child of whatever object this script is on, preferably on the Camera so the objects move with the camera without extra scripting
        topCollider.transform.parent = transform;
        bottomCollider.parent = transform;
        rightCollider.parent = transform;
        leftCollider.parent = transform;
        goal.transform.parent = transform;

        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        //Change our scale and positions to match the edges of the screen (except for the top boundary, which leaves room for the goal above it)  
        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        topCollider.transform.localScale = new Vector3(screenSize.x * 2, 0.1f, colDepth);
        topCollider.transform.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.transform.localScale.y * 5.0f) - colDepth, zPosition);
        bottomCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), zPosition);
        goal.transform.localScale = new Vector3(screenSize.x * 2, colDepth-1, colDepth);
        goal.transform.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y - colDepth/4, zPosition);

        //Set the is Trigger setting to true, so that an object enters the goal we can enter the function on onTriggerEnter2D.
        goal.GetComponent<BoxCollider2D>().isTrigger = true;

        // Add the script MatchFound to our goal game object
        goal.gameObject.AddComponent<MatchManager>();
            
    }

}

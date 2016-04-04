using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public Transform goal;
    public float colDepth = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;
    private Vector3 cameraPos;

    // Use this for initialization
    void Start () {
        // Intialize our empty gameobject goal
        goal = new GameObject().transform;

        // Name our our gameobject 
        goal.name = "Goal";

        // Add collider to our gameobject
        goal.gameObject.AddComponent<BoxCollider2D>();

        // Make the object the child of the object the script is attached to
        goal.parent = transform;

        //Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        //Change our scale and positions to match the top edge of the screen
        goal.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        goal.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (goal.localScale.y * 0.1f), zPosition);

    }

}

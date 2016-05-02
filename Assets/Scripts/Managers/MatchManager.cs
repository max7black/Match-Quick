﻿using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {
    private int removedIndex;    // the index in the list of the object we are going to remove from the list
    private int index;
    private int theMatchIndex;       // random number that will decide the new object that has the tag "Match"
    public static Vector3 cameraPos;
    private Sprite temp;
    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    private float scale;




    // Use this for initialization
    void Start()
    {
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        scale = screenSize.x / screenSize.y;
    }

    // If an object enters the goal, then check if it's the match. If so then destroy the object and add 1 to the score
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Match")
        {
            // If the score is the intial 0 then we need to set it to 1, so that when we multiply by timeLeft we don't get 0.
            if (ScoreManager.score == 0)        
            {
                ScoreManager.score++;
            }  
            // Multiply the current score by timeLeft
            ScoreManager.score += ((int)((TimeManager.timeLeft)));

            DestoryAndRespawn(collider2D);

            TimeManager.timeLeft += 1.00f;       // reset the time after a match is found

            theMatchIndex = Random.Range(0, CreateObject.numberOfObjects); // pick a random object to be the new match
            for (int i = 0; i < CreateObject.numberOfObjects; i++)
            {
                if (i == theMatchIndex)
                {
                    CreateObject.matchObjects[i].tag = "Match";
                }
                if (i == index && i != theMatchIndex)
                {
                    CreateObject.matchObjects[i].tag = "NotMatch";
                }
            }
            CreateObject.theMatch.GetComponent<SpriteRenderer>().sprite = CreateObject.sprite[theMatchIndex];
        }

        // If object is not a match, but is in the goal then teleport it back to the middle of the 
        else if (collider2D.gameObject.tag == "NotMatch")
        {
            DestoryAndRespawn(collider2D);
            CreateObject.matchObjects[index].tag = "NotMatch";
        }
    }

    void DestoryAndRespawn(Collider2D collider2D)
    {
        // Removes object from the list and destorys the object 
        removedIndex = CreateObject.matchObjects.IndexOf(collider2D.gameObject);
        CreateObject.inGoal[removedIndex] = true;
        CreateObject.matchObjects.RemoveAt(removedIndex);
        Destroy(collider2D.gameObject);

        //Adds a new object back in
        CreateObject.matchObjects.Add(new GameObject());
        index = CreateObject.matchObjects.Count - 1;

        //Switch the Sprite list to the new order 
        temp = CreateObject.sprite[removedIndex];
        CreateObject.sprite.RemoveAt(removedIndex);
        CreateObject.sprite.Add(temp);

        //Fill in new objects old properites
        CreateObject.matchObjects[index].name = "Object " + removedIndex;
        CreateObject.matchObjects[index].AddComponent<SpriteRenderer>();
        CreateObject.matchObjects[index].GetComponent<SpriteRenderer>().sprite = CreateObject.sprite[index];
        CreateObject.matchObjects[index].transform.position = new Vector3(Random.Range(cameraPos.x - (3 * screenSize.x / 4), cameraPos.x + (3 * screenSize.x / 4)),
            Random.Range(cameraPos.y - (3 * screenSize.y / 4), cameraPos.y), 0);
        CreateObject.matchObjects[index].transform.localScale = new Vector3(scale / CreateObject.numberOfObjects, scale / CreateObject.numberOfObjects, colDepth);
        CreateObject.matchObjects[index].AddComponent<PolygonCollider2D>();
        CreateObject.matchObjects[index].AddComponent<Rigidbody2D>();
        CreateObject.matchObjects[index].GetComponent<Rigidbody2D>().gravityScale = 0;
        CreateObject.matchObjects[index].AddComponent<Move>();
        CreateObject.matchObjects[index].AddComponent<ClickAndDrag>();
    }
}

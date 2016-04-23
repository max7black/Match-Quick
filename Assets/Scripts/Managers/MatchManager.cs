﻿using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {
    private int removedIndex;    // the index in the list of the object we are going to remove from the list
    private int theMatch;       // random number that will decide the new object that has the tag "Match"
    public static Vector3 cameraPos;


    // Use this for initialization
    void Awake()
    {
        cameraPos = gameObject.GetComponent<Boundary>().cameraPos;
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
            ScoreManager.score *= ((int)((TimeManager.timeLeft)));

            // Removes object from the list and destorys the object 
            removedIndex = CreateObject.matchObjects.IndexOf(collider2D.gameObject);       
            CreateObject.matchObjects.RemoveAt(removedIndex);
            CreateObject.numberOfObjects -= 1;
            Destroy(collider2D.gameObject);

            TimeManager.timeLeft = 5.00f;       // reset the time after a match is found
        }

        // If object is not a match, but is in the goal then teleport it back to the middle of the 
        if (collider2D.gameObject.tag == "NotMatch")
        {
            collider2D.gameObject.transform.position = new Vector3(cameraPos.x, cameraPos.y, 0);

        }
        theMatch = Random.Range(0, CreateObject.numberOfObjects); // pick a random object to be the new match
        for (int i = 0; i < CreateObject.numberOfObjects; i++) {
            if (i == theMatch)
            {
                CreateObject.matchObjects[i].tag = "Match";
            }
        }
    }

}

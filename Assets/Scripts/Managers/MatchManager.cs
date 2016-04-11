using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour {
    public static bool objectDestroyed;
    private string destroyedName;
	// Use this for initialization
    void Awake()
    {
        objectDestroyed = false;
    }

    // If an object enters the goal, then check if it's the match. If so then destroy the object and add 1 to the score
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Match")
        {
            ScoreManager.score++;
            objectDestroyed = true;
            destroyedName = collider2D.gameObject.name;
            for (int i = 0; i < CreateObject.numberOfObjects; i++)
            {
                if (CreateObject.matchObjects[i].name == destroyedName)
                {

                } 
            }
            Destroy(collider2D.gameObject);
            
            
        }
        if (collider2D.gameObject.tag == "NotMatch")
        {
            collider2D.gameObject.transform.position = new Vector3(Random.Range(-21.0f, 21.0f), Random.Range(-3.5f, 0.0f), 0);

        }
        for (int i = 0; i < CreateObject.numberOfObjects; i++) {
            CreateObject.matchObjects
        }
    }

}

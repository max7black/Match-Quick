using UnityEngine;
using System.Collections;

public class MatchFound : MonoBehaviour {

    // If an object enters the goal, then check if it's the match. If so then destroy the object and add 1 to the score
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Match")
        {
            ScoreManager.score++;
            Destroy(collider2D.gameObject);
        }
    }
}

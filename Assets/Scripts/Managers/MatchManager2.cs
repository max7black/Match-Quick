using UnityEngine;
using System.Collections;

public class MatchManager2 : MonoBehaviour
{
    private int removedIndex;    // the index in the list of the object we are going to remove from the list
    private int index;
    private int theMatchIndex;       // random number that will decide the new object that has the tag "Match"
    public static Vector3 cameraPos;
    private Sprite temp;
    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    private float scale;
    AudioSource audio;

    // Intialize the camear position and screen size variables, as well as setting the scale for the mathing objects.
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

            theMatchIndex = Random.Range(0, CreateObject2.numberOfObjects); // pick a random object to be the new match
            for (int i = 0; i < CreateObject2.numberOfObjects; i++)
            {

                if (i == theMatchIndex)
                {
                    CreateObject2.matchObjects[i].tag = "Match";
                }
                if (i == index && i != theMatchIndex)
                {
                    CreateObject2.matchObjects[i].tag = "NotMatch";

                }
            }
            CreateObject2.theMatch[removedIndex].GetComponent<SpriteRenderer>().enabled = true;
            CreateObject2.theMatch[theMatchIndex].GetComponent<SpriteRenderer>().enabled = false;
        //    audio = CreateObject2.theMatch[theMatchIndex].GetComponent<AudioSource>();
        //    audio.clip = Resources.Load("Sounds/match_ding") as AudioClip;      // Load clip for match 
        }

        // If object is not a match, but is in the goal then teleport it back to the middle of the 
        else if (collider2D.gameObject.tag == "NotMatch")
        {
            DestoryAndRespawn(collider2D);
            CreateObject2.matchObjects[index].tag = "NotMatch";
       //     audio.clip = Resources.Load("Sounds/Wrong_sound") as AudioClip;      // Load clip for wrong sound
        //    audio = CreateObject2.theMatch[index].GetComponent<AudioSource>();
        }
      //  audio.Play();
    }

    void DestoryAndRespawn(Collider2D collider2D)
    {
        // Removes object from the list and destorys the object 
        removedIndex = CreateObject2.matchObjects.IndexOf(collider2D.gameObject);
        CreateObject2.inGoal[removedIndex] = true;
        CreateObject2.matchObjects.RemoveAt(removedIndex);
        Destroy(collider2D.gameObject);

        //Adds a new object back in
        CreateObject2.matchObjects.Add(new GameObject());
        index = CreateObject2.matchObjects.Count - 1;

        //Switch the Sprite list to the new order 
        temp = CreateObject2.sprite[removedIndex];
        CreateObject2.sprite.RemoveAt(removedIndex);
        CreateObject2.sprite.Add(temp);

        //Fill in new objects old properites
        CreateObject2.matchObjects[index].name = "Object " + removedIndex;
        CreateObject2.matchObjects[index].AddComponent<SpriteRenderer>();
        CreateObject2.matchObjects[index].GetComponent<SpriteRenderer>().sprite = CreateObject2.sprite[index];
        CreateObject2.matchObjects[index].transform.position = new Vector3(Random.Range(cameraPos.x - (3 * screenSize.x / 4), cameraPos.x + (3 * screenSize.x / 4)),
            Random.Range(cameraPos.y - (3 * screenSize.y / 4), cameraPos.y), 0);
        CreateObject2.matchObjects[index].transform.localScale = new Vector3(scale / CreateObject2.numberOfObjects, scale / CreateObject2.numberOfObjects, colDepth);
        CreateObject2.matchObjects[index].AddComponent<PolygonCollider2D>();
        CreateObject2.matchObjects[index].AddComponent<Rigidbody2D>();
        CreateObject2.matchObjects[index].GetComponent<Rigidbody2D>().gravityScale = 0;
        CreateObject2.matchObjects[index].AddComponent<Move2>();
        CreateObject2.matchObjects[index].AddComponent<ClickAndDrag>();
    //    CreateObject2.matchObjects[index].AddComponent<AudioSource>();
    }
}

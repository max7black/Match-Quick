using UnityEngine;
using System.Collections;

public class MatchManager2 : MonoBehaviour
{
    private int removedIndex;    // the index in the list of the object we are going to remove from the list
    private int theMatchIndex;       // random number that will decide the new object that has the tag "Match"
    public static Vector3 cameraPos;
    public float colDepth = 4f;
    public float zPosition = 0f;
    public Vector2 screenSize;
    public new AudioSource audio;
    public GameObject tempObject;

    // Intialize the camera position and screen size variables, as well as setting the scale for the mathing objects.
    void Start()
    {
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
    }

    // If an object enters the goal, then check if it's the match. If so then destroy the object and add 1 to the score
    void OnTriggerEnter2D(Collider2D collider2D)
    {    
        if (collider2D.gameObject.tag == "Match")
        {
            // get the audio source for the current match
            audio = CreateObject2.theMatch[theMatchIndex].GetComponent<AudioSource>();

            // Add 10 to the score since there was a match
            ScoreManager.score += 10;
            // Add a time bonus score to the score that depends on the how quickly they matched the object
            ScoreManager.score += (int)(20.0f - (TimeManager.lastMatchTime - TimeManager.timeLeft));
            TimeManager.lastMatchTime = TimeManager.timeLeft;

            DestoryAndRespawn(collider2D);

            TimeManager.timeLeft += 1.00f;       // reset the time after a match is found

            theMatchIndex = Random.Range(0, CreateObject2.numberOfObjects); // pick a random object to be the new match
            
            for (int i = 0; i < CreateObject2.numberOfObjects; i++)
            {
                // if the i = theMatchIndex then in the list matchObjects set the tag at index i equal to "Match"
                if (i == theMatchIndex)
                {
                    CreateObject2.matchObjects[i].tag = "Match";
                }
            }
            CreateObject2.theMatch[removedIndex].GetComponent<SpriteRenderer>().enabled = true;
            CreateObject2.theMatch[theMatchIndex].GetComponent<SpriteRenderer>().enabled = false;
            audio.PlayOneShot(Resources.Load("Sounds/match_ding") as AudioClip, SoundManager.hSliderValue);
        }

        // If object is not a match, but is in the goal then teleport it back to the middle of the 
        else if (collider2D.gameObject.tag == "NotMatch")
        {
            DestoryAndRespawn(collider2D);
            // if-else makes sure audio component we get is not from the Match
            if (theMatchIndex != 0)
            {
                audio = CreateObject2.theMatch[0].GetComponent<AudioSource>();
            }
            else
            {
                audio = CreateObject2.theMatch[1].GetComponent<AudioSource>();
            }
            audio.PlayOneShot(Resources.Load("Sounds/Wrong_sound") as AudioClip, SoundManager.hSliderValue);
        }
    }


    // Removes object from the list and destroys the object. Then inserts a clone into list in the old objects spot.
    // It's necessary to destroy the object and make a new one rather then simply teleporting it back out of the "goal area"
    // because the ClickAndDrag script won't let you change the position while your holding onto it.
    void DestoryAndRespawn(Collider2D collider2D)
    {
        removedIndex = CreateObject2.matchObjects.IndexOf(collider2D.gameObject);
        tempObject = Instantiate(CreateObject2.matchObjects[removedIndex]);
        tempObject.name = CreateObject2.matchObjects[removedIndex].name;// set the name back to the orignal (otherwise it adds clone everytime it's cloned)
        CreateObject2.matchObjects.RemoveAt(removedIndex);              // remove old object from queue
        Destroy(collider2D.gameObject);                                 // Destroy the object
        CreateObject2.matchObjects.Insert(removedIndex, tempObject);    // put the new clone into old objects spot
        CreateObject2.matchObjects[removedIndex].tag = "NotMatch";      // make sure the objects tag is set to NotMatch 
        CreateObject2.matchObjects[removedIndex].transform.position = new Vector3(Random.Range(cameraPos.x - (3 * screenSize.x / 4), cameraPos.x + (3 * screenSize.x / 4)),
            Random.Range(cameraPos.y - (3 * screenSize.y / 4), cameraPos.y), 0);
    }

}

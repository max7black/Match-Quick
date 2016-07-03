using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]

public class ClickAndDrag : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    // Entered when mouse is clicked and checks if mouse was clicked on the gameobject.
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    // Enter if the mouse if dragged after being clicked and If the mouse was clicked on the object it changes the objects position 
    // to be the same as the cursors.
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        

    }
}

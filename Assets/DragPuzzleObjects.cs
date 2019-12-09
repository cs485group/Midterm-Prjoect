using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EXPERIMENT: Try to use Waypoints for the objects to drag. We will set Waypoints (empty gameObjects)
//      as the only positions that the objects can be moved to. It will go like this:
//          1. Set up the puzzle area: create Waypoints (7 for the 7 puzzle pieces, 1 for the empty
//              space that the pieces will move to)
//          2. 'Connect' the Waypoints to each other in a specific way (will probably be managed
//              by a bunch of 'if' statements to check if any 'adjacent' Waypoint is empty)
//          3. Mark each Waypoint to correspond to a specific puzzle piece (indicated by color?)
//              Purple space -> purple piece, red space -> red piece, etc.
//          4. Click a puzzle object (any of them)
//              a. If an adjacent Waypoint (space) is empty, move to it
//              b. Otherwise, stay on the current space.
//          5. Keep at step #4 until all puzzle pieces are on their respective spaces
//          6. Puzzle is completed
//  I should try to get all this implemented on a different script, and then
//  create a reference here? 

public class DragPuzzleObjects : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    void OnMouseDown() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    private Vector3 GetMouseWorldPos() {
        // pixel coordinates (x, y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag() {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}

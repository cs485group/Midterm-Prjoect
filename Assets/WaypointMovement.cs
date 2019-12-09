using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Waypoint {
    public GameObject piece; //the occupying piece
    public GameObject colorSpace; //the color of the space the waypoint is at; this never changes
    public GameObject[] connections; //the list of connected waypoints to this waypoint
    public bool isOccupied; //whether the waypoint is occupied
    public bool rightPiece; //if a colored piece is on its' correct waypoint
    public int idSpace; //the 'ID' of the waypoint space. Indicated in 'colorSpace'.
    public int idPiece; //the 'ID' of the piece on the space. Indicated in 'piece'. 0 = no piece
}

public class WaypointMovement : MonoBehaviour
{
    Waypoint[] waypoints = new Waypoint[8]; //list of all waypoints in the puzzle
    public GameObject SelectedObject; //GameObject the player selected (for the pieces only)
    public GameObject[] redConnections1; //waypoints connected to RedSpot1
    public GameObject[] blueConnections2; //waypoints connected to BlueSpot2
    public GameObject[] yellowConnections3; //waypoints connected to YellowSpot3
    public GameObject[] greenConnections4; //waypoints connected to GreenSpot4
    public GameObject[] orangeConnections5; //waypoints connected to OrangeSpot5
    public GameObject[] purpleConnections6; //waypoints connected to PurpleSpot6
    public GameObject[] pinkConnections7; //waypoints connected to PinkSpot7
    public GameObject[] emptyConnections8; //waypoints connected to EmptySpot8
    int inPuzzle; //checking whether or not the player is in the puzzle; 0 = no, 1 = yes
    SelectionManager selectionManager; //allows to interact with SelectionManager script
    bool allCorrectSpots; //checks to see if all pieces are in their correct spots.
    bool stopMessage; //for the if statement in 'Update' that manages the puzzle completion
 
    // Start is called before the first frame update
    void Start()
    {
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();

        //creating the waypoints - 
        //Piece on waypoint; waypoint space color; index for 'waypoints' array;
        //whether or not the space is occupied; whether or not the right piece is occupying it
        //(string, string, int, bool, bool)
        creatingWaypoints("PurpleRook6", "RedSpot1", 0, true, false, redConnections1);
        creatingWaypoints("GreenRook4", "BlueSpot2", 1, true, false, blueConnections2);
        creatingWaypoints("OrangeRook5", "YellowSpot3", 2, true, false, yellowConnections3);
        creatingWaypoints("PinkRook7", "GreenSpot4", 3, true, false, greenConnections4);
        creatingWaypoints("YellowRook3", "OrangeSpot5", 4, true, false, orangeConnections5);
        creatingWaypoints("RedRook1", "PurpleSpot6", 5, true, false, purpleConnections6);
        creatingWaypoints("BlueRook2", "PinkSpot7", 6, true, false, pinkConnections7);
        creatingWaypoints("EmptyRook8", "EmptySpot8", 7, false, true, emptyConnections8);
        allCorrectSpots = false;
        stopMessage = false;
        inPuzzle = 0;
    }

    void creatingWaypoints(string piece, string space, int arrayPos, bool occupied, bool rightSpot, GameObject[] wConnections) {
        waypoints[arrayPos].piece = GameObject.Find(piece);
        waypoints[arrayPos].colorSpace = GameObject.Find(space);
        waypoints[arrayPos].isOccupied = occupied;
        waypoints[arrayPos].rightPiece = rightSpot;
        waypoints[arrayPos].connections = wConnections;
        waypoints[arrayPos].idSpace = int.Parse(space.Substring(space.Length - 1));
        waypoints[arrayPos].idPiece = int.Parse(piece.Substring(piece.Length - 1));
    }
    // Update is called once per frame
    void Update()
    {
        //message for completing puzzle
        if(allCorrectSpots == true && !stopMessage) {
            Debug.Log("Congrats");
            selectionManager.puzzleCompleted(true);
            stopMessage = true; //will make it so that this if statment is only executed once
        }
        //if pressed left-click, player is in puzzle and has not completed it yet
        if(Input.GetMouseButtonDown(0) && inPuzzle == 1 && !allCorrectSpots) {
            GameObject temp;
            bool movedPiece = false;
            string idStr; //id number on current waypoint connection being looked at
            int id; //integer form of 'idStr'
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) //hit an object
            {
                SelectedObject = hit.transform.gameObject;
                for(int i = 0; i < waypoints.Length; ++i) { //checking 'waypoints' array
                    if(waypoints[i].piece.name == SelectedObject.name) {
                        //checking 'connections' array in current 'waypoints' element
                        //to see if there are any adjacent empty spaces; if not, do nothing
                        for(int j = 0; j < waypoints[i].connections.Length; ++j) {
                            //getting the id of the connected waypoint
                            temp = waypoints[i].connections[j];
                            idStr = temp.name.Substring(temp.name.Length - 1);
                            id = int.Parse(idStr); //will be 1 through 8

                            //checking if the waypoint referred by connections[j] is occupied
                            if(waypoints[id - 1].isOccupied == false) { //move the piece
                                swap(i, id - 1);
                                movedPiece = true;
                                break;
                            }
                            else {
                                //do nothing; current connection is occupied right now
                            }
                        }
                    }
                    if(movedPiece) { //break out of for loop; already got selected piece moved
                        break;
                    }
                }
            }
            
            //checking if all waypoints have the correct piece on them
            allCorrectSpots = true; //assume all pieces on correct spots
            for(int k = 0; k < waypoints.Length; ++k) {
                Debug.Log("Waypoint[" + k + "] has " + waypoints[k].piece.name + ": is correct - " + 
                    waypoints[k].rightPiece + "; idPiece = " + waypoints[k].idPiece + ", idSpace = " + waypoints[k].idSpace);
                if(waypoints[k].rightPiece == false) {
                    allCorrectSpots = false; //find out not all pieces in correct spots
                    break; //leave for loop; no need to search any further
                }
            }
        }
    }
    void swap(int occupiedWaypoint, int emptyWaypoint) { //swaps the values on two Waypoint elements based on array values passed here
        //moving the piece to 'emptyWaypoint'
        Debug.Log("Piece is: " + waypoints[occupiedWaypoint].piece.name);
        Debug.Log("Moving to: " + waypoints[emptyWaypoint].colorSpace.name);

        Waypoint temp;
        temp = waypoints[occupiedWaypoint];

        //Swapping the values 
        waypoints[occupiedWaypoint].piece.transform.position = waypoints[emptyWaypoint].colorSpace.transform.position;
        waypoints[occupiedWaypoint].piece = waypoints[emptyWaypoint].piece;
        waypoints[occupiedWaypoint].isOccupied = false;
        waypoints[occupiedWaypoint].idPiece = waypoints[emptyWaypoint].idPiece;
        if(waypoints[occupiedWaypoint].idPiece == waypoints[occupiedWaypoint].idSpace) { 
            waypoints[occupiedWaypoint].rightPiece = true;
        }
        else {
            waypoints[occupiedWaypoint].rightPiece = false;
        }

        waypoints[emptyWaypoint].piece.transform.position = temp.colorSpace.transform.position;
        waypoints[emptyWaypoint].piece = temp.piece;
        waypoints[emptyWaypoint].isOccupied = true;
        waypoints[emptyWaypoint].idPiece = temp.idPiece;
        //piece is moving to 'waypoints[emptyWaypoint]'; need to check if right piece got there.
        //Piece is in right place if idPiece = idSpace.
        if(waypoints[emptyWaypoint].idPiece == waypoints[emptyWaypoint].idSpace) { 
            waypoints[emptyWaypoint].rightPiece = true;
        }
        else {
            waypoints[emptyWaypoint].rightPiece = false;
        }
    }

    public void playerInteractedPuzzle(int newState) { //checking whether player entered or exited puzzle
       inPuzzle = newState;
    }
}

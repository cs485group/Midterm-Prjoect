using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Camera mainCamera;
    public GameObject player; //the player
    public GameObject SelectedObject; //the object the player selected
    public GameObject Pedestal; //the pedestal where the puzzle game occurs
    public GameObject theDoor; //the door that will be opened when puzzle is completed
    private float dist; //the distance between 'player' and 'SelectedObject' (above)
    private float distPuzzleGame; //distance 'player' is away from where the puzzle game is
    private float speed; //speed of 'theDoor' opening
    private float step; //calculate speed for moving 'theDoor'
    private int fromPlayerToPuzzleMax; //maximum distance 'player' needs to be from the puzzle
                                       //located on 'Pedestal' to start the puzzle
    private bool doorOpen; //Checking if the door is open from solving puzzle
    private bool inPuzzle; //checking if the player is currently playing the puzzle
    private bool onTheGround; //checks if player is on the ground
    private Vector3 newPosition; //position 'theDoor' moves to when opened
    private Vector3 mainCamOrigRot; //original rotation of 'mainCamera'
    private Vector3 mainCamPuzzlePos; //position of 'mainCamera' when on puzzle
    private Vector3 mainCamPuzzleRot; //rotation of 'mainCamera' when on puzzle
    private FirstPerson_MovingOnly firstPerson_MovingOnly; //allows access to 'FirstPerson_MovingOnly'
                                                           //script; will be used to allow player to move
    private WaypointMovement waypointMovement; //movement script for puzzle pieces; object of that script
                                               //is here so that we can allow those pieces to move based
                                               //on whether or not the player is in the puzzle
   
    void Start() {
        //FirstPerson Script access
        firstPerson_MovingOnly = GameObject.FindObjectOfType<FirstPerson_MovingOnly>();
        //WaypointMovement Script access
        waypointMovement = GameObject.FindObjectOfType<WaypointMovement>();

        //number values for stuff like speed of door, distance from selected object,
        //position of door when it gets opened, etc.
        speed = 6f;
        step = speed * Time.deltaTime;
        newPosition = theDoor.transform.position - new Vector3(0, 11, 0);
        dist = 100;
        fromPlayerToPuzzleMax = 5;

        //boolean values
        doorOpen = false;
        inPuzzle = false;
        onTheGround = true;

        //Setting details for 'mainCamera'; which camera it gets, its' positions, etc.
        mainCamera = Camera.main; //'mainCamera' gets... the main camera
        mainCamOrigRot = new Vector3(0, 0, 0);
        mainCamPuzzlePos = new Vector3(Pedestal.transform.position.x, 
                Pedestal.transform.position.y + 3, Pedestal.transform.position.z - 2.5f);
        mainCamPuzzleRot = new Vector3(50, 0, 0);

        //setting 'mainCamera' properties
        /*mainCamera.transform.position = new Vector3(player.transform.position.x + 0.1f, 
                player.transform.position.y + 1.6f, player.transform.position.z - 5);
        mainCamera.transform.eulerAngles = mainCamOrigRot;*/
    }
    // Update is called once per frame
    void Update()
    {
        distPuzzleGame = Vector3.Distance(player.transform.position, Pedestal.transform.position);
        //get input from mouse; 1 = right click
        if(Input.GetKeyDown(KeyCode.X)) {
            if(inPuzzle) { //exiting out of puzzle
                //restoring regular camera conditions
                Debug.Log("Exiting Out of Puzzle");
                mainCamera.transform.position = new Vector3(player.transform.position.x + 0.1f, 
                        player.transform.position.y + 1.6f, player.transform.position.z - 5);
                mainCamera.transform.eulerAngles = mainCamOrigRot;

                inPuzzle = false; //no longer in puzzle
                waypointMovement.playerInteractedPuzzle(0); //disable interaction with puzzle pieces
                firstPerson_MovingOnly.allowMovement(true); //enable player movement
            }
            else {
                //Establishing RaycastHit for being able to interact with game objects with mouse
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) 
                {
                    SelectedObject = hit.transform.gameObject;
                    dist = Vector3.Distance(player.transform.position, SelectedObject.transform.position);
                    Debug.Log("We hit " + hit.collider.name + " from " + dist + " units away."); //it hits whenever it detects any GameObject, this 'if' statement
                    if(SelectedObject.name == Pedestal.name) 
                    {
                        onTheGround = firstPerson_MovingOnly.playerOnGround();
                        Debug.Log("onTheGround is " + onTheGround);
                        //close enough to puzzle
                        if(dist <= fromPlayerToPuzzleMax && !inPuzzle && !doorOpen && onTheGround) {
                            //Adjusting camera to be above puzzle area
                            mainCamera.transform.position = mainCamPuzzlePos;
                            mainCamera.transform.eulerAngles = mainCamPuzzleRot;

                            //Allows player to interact with the puzzle pieces
                            waypointMovement.playerInteractedPuzzle(1); 
                            //disable player movement
                            firstPerson_MovingOnly.allowMovement(false);

                            inPuzzle = true; //player is in puzzle
                            Debug.Log("Starting the puzzle.");
                        }
                    }  
                }
            }
        }
        //the if statement that manages the door opening event
        if(doorOpen) {
            theDoor.transform.position = Vector3.MoveTowards(theDoor.transform.position, newPosition, step);
        }  
    }
    public void puzzleCompleted(bool isComplete) { //checking whether puzzle is completed
        doorOpen = isComplete;
        if(doorOpen) {
            Debug.Log("Got the Door Down; player completed the puzzle.");
        }
    }
    void OnGUI() {
        if(distPuzzleGame <= fromPlayerToPuzzleMax && !inPuzzle && !doorOpen) {
            GUI.Box(new Rect((Screen.width / 2) - 100, Screen.height - 100, 200, 30), 
                    "Press 'x' to begin the puzzle");
        }
        if(inPuzzle) {
            GUI.Box(new Rect((Screen.width / 2) - 100, Screen.height - 100, 200, 30), 
                    "Press 'x' to exit the puzzle");
        }
    }
}

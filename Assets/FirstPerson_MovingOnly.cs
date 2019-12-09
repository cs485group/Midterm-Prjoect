using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson_MovingOnly : MonoBehaviour
{
    public Transform feet; //part of Player object to determine where to stop in relation to 'ground' layer
    public LayerMask ground; //makes a 'ground' layer; used to make an actual ground
    private Vector3 direction; //allows for movement
    //class of first object
    private Rigidbody rbody;
    private float jumpHeight; //distance a 'jump' action will do
    private int doubleJump = 0; //variable for allowing to jump twice
    private float speed = 7f; //how fast the Player will move
    private float rotationSpeed = 1f; //how fast the Player will rotate, or 'look around'
    private float rotationX = 0;
    private float rotationY = 10f;
    private bool canMove; //variable for allowing movement of player
    private bool onGround; //checking if player on ground for other scripts' purposes

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        jumpHeight = 5.0f;
        canMove = true;
        onGround = true;
    }

    //add in a layer called 'Ground' in Plane object; set Plane to that new Layer
    //Add in Empty Game Object. Put it as a child of the object you will be moving.

    //In the script part for the object that has this, assign feet to 'Feet' object,
    //The Ground part will also be the Ground Layer we created

    // Update is called once per frame
    void Update()
    {
        if(canMove) {
            direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal");
            direction.z = Input.GetAxis("Vertical");
            direction = direction.normalized;
            if (direction.x != 0)
                rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
            if (direction.z != 0)
                rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);

            //allows for smooth rotation view from mouse input
            //rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
            //rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
            //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            bool isGrounded()
            {
                if (Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore)) 
                {
                    doubleJump = 0;
                    onGround = false;
                    return true;
                }
                else {
                    return false;
                }
            }
            if (Input.GetButtonDown("Jump") && (isGrounded() || doubleJump < 2))
            {
                doubleJump += 1;
                rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            }
            //checking if on the ground
            if (Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore)) 
            {
                onGround = true;
            }
            else {
                onGround = false;
            }
        } 
    }
    public void allowMovement(bool moveAllow) { //function that allows player character to move or not
        canMove = moveAllow;
    }
    public bool playerOnGround() {
        return onGround;
    }
}

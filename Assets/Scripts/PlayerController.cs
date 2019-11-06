using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public Rigidbody theRB;
    public float jumpForce;
    //public CharacterController controller;
    public float gravityScale;
    public Transform playerModel;
    private Vector3 direction;

    public Transform Camera;

    private Vector3 moveDirection;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;
    

    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    private LoZStyle2 script1;
    private CameraController script2;


	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame (THIS IS A LOOP)
	void LateUpdate () {
        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        
        /*if(Input.GetButtonDown("Jump"))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }
        */
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        //print("|CamRot > " + Camera.rotation.eulerAngles + "  |CurrentRot > " + playerModel.transform.rotation.eulerAngles);
    
        //moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        theRB.MovePosition(theRB.position + moveDirection * Time.deltaTime);
        //controller.Move(moveDirection * Time.deltaTime);
        //Move the player in different directions based on camera look direction
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }


        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical"))) + (Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
        anim.SetBool("Swing", Input.GetButtonDown("Fire1"));
    }
}

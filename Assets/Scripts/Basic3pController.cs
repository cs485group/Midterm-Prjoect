using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic3pController : MonoBehaviour
{
    //public Transform feet;
    public LayerMask Ground;
    public Transform Camera;
    public Transform playerModel;
    public Transform pivot;



    private Vector3 direction;
    private Rigidbody rBody;
    private float jumpHeight;
    private int jumpCount = 0;
    private float speed = 7f;
    private Vector3 velocity;

    private float temp;

    private AudioSource audio1;
    private AudioSource audio2;

    private LoZStyle2 script1;
    private CameraController script2;

    public float rotateSpeed;

    private Quaternion rotX;
    private Quaternion rotZ;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        jumpHeight = 3.0f;
        //audio1 = GetComponents<AudioSource>()[0];
        //audio2 = GetComponents<AudioSource>()[1];
        velocity = Vector3.zero;
        direction = Vector3.zero;

        script1 = GetComponent<LoZStyle2>();
        script2 = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;

        

        if(direction.x != 0)
            {
                rBody.MovePosition(rBody.position + Camera.right * direction.x * speed * Time.deltaTime);
                rotX = Quaternion.LookRotation(rBody.position + Camera.right * direction.x * speed * Time.deltaTime);
                
            }
        if(direction.z != 0)
            {   
                rBody.MovePosition(rBody.position + pivot.forward*direction.z * speed * Time.deltaTime);
                rotZ = Quaternion.LookRotation(rBody.position + pivot.forward*direction.z * speed * Time.deltaTime);
               
            }
        print("|CamRot > " + Camera.rotation.eulerAngles + "  |CurrentRot > " + playerModel.transform.rotation.eulerAngles);
            
        //Quaternion trueRot = rotX.eulerAngles + rotZ.eulerAngles;

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        
    } 

}

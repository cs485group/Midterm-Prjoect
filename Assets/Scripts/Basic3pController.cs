using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic3pController : MonoBehaviour
{
    public Transform feet;
    public LayerMask Ground;
    public Transform Camera;
    public Transform playermodel;

    private Vector3 direction;
    private Rigidbody rBody;
    private float jumpHeight;
    private int jumpCount = 0;
    private float speed = 7f;
    private Vector3 velocity;

    private float temp;

    private AudioSource audio1;
    private AudioSource audio2;



    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        jumpHeight = 3.0f;
        audio1 = GetComponents<AudioSource>()[0];
        audio2 = GetComponents<AudioSource>()[1];
        velocity = Vector3.zero;
        direction = Vector3.zero;
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

            }
        if(direction.z != 0)
            {   
                rBody.MovePosition(rBody.position + Camera.forward*direction.z * speed * Time.deltaTime);
               
            }

        //playermodel.rotation = Quaternion.LookRotation(direction);    


        //transform.rotation = Quaternion.LookRotation(rBody.velocity);

        //transform.localEulerAngles = new Vector3(direction.x, 0, direction.z);

        bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, Ground, QueryTriggerInteraction.Ignore);
        

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                //audio1.Play();
                jumpCount += 1;
                rBody.AddForce(Vector3.up * Mathf.Sqrt(-jumpHeight*Physics.gravity.y), ForceMode.VelocityChange);
            }
        

        

    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public int flag = 0;
   
    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;
    public bool invertX;

    // Use this for initialization
    void Start () {
        print("HUP");

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        //pivot.transform.parent = null;

        //Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame (LateUpdate makes it so its after another object/ helps makes camera smoother)
	void Update () {
        //print(offset);

        pivot.transform.position = target.transform.position;
        

        //get the x position of the mouse and rotate the target(now pivot)
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //target.Rotate(0, horizontal, 0);

        if (invertX)
        {
            pivot.Rotate(0, -horizontal, 0);
        }
        else
        {
            pivot.Rotate(0, horizontal, 0);
        }

        //move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        //print("|>> " + rotation.eulerAngles);
        transform.position = target.position - (rotation * offset);


        //transform.position = target.position - offset;
/* 
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, pivot.position.y - 0.5f, transform.position.z);
        }
*/
        transform.LookAt(target);
	
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoZStyle2 : MonoBehaviour
{
    public Transform target;
    //public Transform pivot;
    //public float rotationSpeed;

    //private float CamDistance;
    private Vector3 offset;
    private int flag;
    private float height;

    private float horizontal;
    //private float verticle;

    // Start is called before the first frame update
    void Start()
    {
        //flag = 0;
        //pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        //offset = target.position - transform.position;
        //pivot.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.up);

       
            //transform.position = new Vector3(target.position.x + offset.x, transform.position.y,target.position.z + offset.z);
    }
}

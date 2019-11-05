using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform target;
    
    //private float CamDistance;
    private Vector3 offset;
    private int flag;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        height = target.position.y + 1f;
        //print(dist);

        

        if(dist >= 5f)
        {
            if(flag == 0)
            {
                offset = transform.position - target.position;
                flag = 1;
            }
            else
            {
            
             transform.position = new Vector3(target.position.x + offset.x, height, target.position.z + offset.z);
             
            
            //transform.position = new Vector3(target.position.x + offset.x, transform.position.y,target.position.z + offset.z);
             }
            //print("out of range");
        }
        else
        {
            transform.LookAt(target, Vector3.up);
            flag = 0;
        }

        //if(dist >= transform.position)
            //transform.position = target.position - offset;
        //transform.LookAt(target);
    }
}

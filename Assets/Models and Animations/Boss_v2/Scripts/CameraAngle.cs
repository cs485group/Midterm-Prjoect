using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;    
    // Start is called before the first frame update
    void Start()
    {
        camera2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        camera1.enabled = false;
        camera2.enabled = true;
    }

}

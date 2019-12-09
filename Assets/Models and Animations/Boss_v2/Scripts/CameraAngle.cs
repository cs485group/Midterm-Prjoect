using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;    
    // Start is called before the first frame update
    void Start()
    {
        camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }

}

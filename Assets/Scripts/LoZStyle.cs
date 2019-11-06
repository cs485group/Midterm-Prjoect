using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoZStyle : MonoBehaviour
{
   private LoZStyle2 script1;
   private CameraController script2;

   private Basic3pController pScript1;
   private PlayerController pScript2;

    private GameObject Camera;
    private GameObject player;

   public Transform target;
   public Transform Cam;

   public Transform pivot;

   private Vector3 prev;

   int flag = 0;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");

        script1 = Camera.GetComponent<LoZStyle2>();
        script2 = Camera.GetComponent<CameraController>();

        pScript1 = player.GetComponent<Basic3pController>();
        pScript2 = player.GetComponent<PlayerController>();

        pScript1.rotateSpeed = pScript2.rotateSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, Cam.position);
        pivot.transform.position = target.transform.position;

        if(dist < 10f || Input.GetAxisRaw("Vertical") < 0)
        {
            //print("|1 > " + Cam.position);
            script1.enabled = true;
            script2.enabled = false;

            pScript1.enabled = true;
            pScript2.enabled = false;
            flag = 0;

            Quaternion rotation = Quaternion.Euler(0f, Cam.rotation.eulerAngles.y, 0f);
            pivot.rotation = rotation;

            prev = Cam.position;
        }
        else
        {
            if(flag == 0)
            {
                //print("|2 > " +Cam.position);

                //script2.offset = pivot.position - prev;
                Quaternion rotation = Quaternion.Euler(0f, Cam.rotation.eulerAngles.y, 0f);
                pivot.rotation = rotation;

                flag = 1;
            }

            script1.enabled = false;
            script2.enabled = true;

            pScript1.enabled = false;
            pScript2.enabled = true;
        }
    }
}

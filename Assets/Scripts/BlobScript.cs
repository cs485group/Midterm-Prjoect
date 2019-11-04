using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobScript : MonoBehaviour
{
    private bool isDead = false;
    private bool grounded = true;
    private Rigidbody rbody;
    public LayerMask ground;
    public Transform Player;
    public Transform Blob;
    public GameObject PlayerObject;
    private Animator anim;

    int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 0;

    public int buttonWidth;
    public int buttonHeight;
    private int origin_x;
    private int origin_y;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        buttonWidth = 200;
        buttonHeight = 50;
        origin_x = Screen.width / 2 - buttonWidth / 2;
        origin_y = Screen.height / 2 - buttonHeight * 2;
    }

    void Update()
    {
        
        
        if (Vector3.Distance(transform.position, Player.position) >= MinDist & Vector3.Distance(transform.position, Player.position) <= MaxDist & isGrounded())
        {
            if(anim.enabled == false)
                anim.enabled = true;
            transform.LookAt(Player);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            anim.Play("Bounce", MoveSpeed);
        }
        else
        {
            anim.enabled = false;
        }

        bool isGrounded()
        {
            if (Physics.CheckSphere(Blob.position, .8f, ground, QueryTriggerInteraction.Ignore))
            {
   
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(PlayerObject);
            isDead = true;

        }
    }
    public void OnGUI()
    {
        if (isDead == true)
        {
            if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 2 + 20, buttonWidth, buttonHeight), "Restart"))
            {
                Application.LoadLevel("ThirdPerson");
            }
            if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 3 + 30, buttonWidth, buttonHeight + 10), "Exit"))
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();

            }
        }
        else
        {

        }

    }
}

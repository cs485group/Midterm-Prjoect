using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public Rigidbody theRB;
    public float jumpForce;
    //public CharacterController controller;
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

    private int injured;

    [SerializeField]
    private int previousWeapon;

    public GameObject sword;
    public GameObject hammer;
    public GameObject spear;

    public int MaxHP;
    private int previousHP;
    [SerializeField]
    private int healthPoints;
    
    private GameObject[] hearts;

    private int state;
    private int swingState;

    public GameObject empty;
    public GameObject Icon1;
    public GameObject Icon2;
    public GameObject Icon3;

    private int weaponDamage;

    public GameObject boss;
    public GameObject hOrb1;
    public GameObject hOrb2;

    [SerializeField]
    private int attacking;

	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody>();
        previousWeapon = 1;
        //controller = GetComponent<CharacterController>();
        sword.SetActive(false);
        hammer.SetActive(false);
        spear.SetActive(false);
        healthPoints = MaxHP - 1;
        previousHP = healthPoints;
        state = 0;
        swingState = 1;
        hearts = GameObject.FindGameObjectsWithTag("HeartPiece");
        empty.SetActive(true);
        Icon2.SetActive(false);
        Icon3.SetActive(false);
        Icon1.SetActive(false);
    
        weaponDamage = 0;

        attacking = 0;

        injured = 0;

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame (THIS IS A LOOP)
	void Update () {




        if(injured == 1)
        {
            knockbackCounter += Time.deltaTime;

            if(knockbackCounter > knockbackTime)
            {
                injured = 0;
                knockbackCounter = 0;
            }
        }

        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        
        /*if(Input.GetButtonDown("Jump"))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }
        */
        if(Input.GetButtonDown("TestKey"))
        {
            HurtPlayer(1);
        }

        if(Input.GetButtonDown("HotKey1"))
        {
            weaponDamage = 1;
            anim.SetBool("2h",false);
            hammer.SetActive(false);
            spear.SetActive(false);
            sword.SetActive(true);
            anim.SetBool("Sword", true);
            state = 1;
            empty.SetActive(false);
            Icon2.SetActive(false);
            Icon3.SetActive(false);
            Icon1.SetActive(true);
            
        }
        else if(Input.GetButtonDown("HotKey2"))
        {
            weaponDamage = 3;
            anim.SetBool("Sword", false);
            sword.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(true);
            anim.SetBool("2h",true);
            state = 2;
            empty.SetActive(false);
            Icon1.SetActive(false);
            Icon3.SetActive(false);
            Icon2.SetActive(true);
        }
        else if(Input.GetButtonDown("HotKey3"))
        {
            weaponDamage = 2;
            anim.SetBool("Sword", false);
            sword.SetActive(false);
            hammer.SetActive(false);
            spear.SetActive(true);
            anim.SetBool("2h",true);
            state = 3;
            empty.SetActive(false);
            Icon1.SetActive(false);
            Icon2.SetActive(false);
            Icon3.SetActive(true);
        }
        anim.SetInteger("State",state);


        if(Input.GetButtonDown("Fire1"))
        {
            attacking = 1;
            attack();
        }


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
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(-moveDirection.x, 0f, -moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }


        anim.SetFloat("RUN", (Mathf.Abs(Input.GetAxisRaw("Vertical"))) + (Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
    }


    public void HurtPlayer(int damage)
    {
        if(injured == 0)
        {
            injured = 1;
            for(int i = healthPoints; i > (healthPoints - damage); i--)
                hearts[i].SetActive(false);

            healthPoints -= damage;   

            if(healthPoints < 0)
                Application.LoadLevel(Application.loadedLevel); 
        }
    }

    public void animFin()
    {
        attacking = 0;
        anim.SetBool("Swing", false);
        anim.SetBool("Hammer_Swing", false);
        anim.SetBool("Swing", false);
    }

    private void attack()
    {
        if(state == 1)
        {
            anim.SetBool("Swing", true);
        }
        if(state == 2 || state == 3)
        {
            anim.SetBool("Hammer_Swing", true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(attacking == 1)
        {       
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Ehealthbar>().injure(weaponDamage);
            }
            if(collision.gameObject.tag == "Boss")
            {
                boss.GetComponent<BossScript>().injure(weaponDamage);
            }
            if(collision.gameObject.tag == "Orb")
            {
                hOrb1.GetComponent<HeadMovement>().injure(weaponDamage);
            }
            if(collision.gameObject.tag == "Orb2")
            {
                hOrb2.GetComponent<HeadMovement>().injure(weaponDamage);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator anim;

    public int maxHp;
    [SerializeField]
    private int currentHp;

    public float actionCounter;

    [SerializeField]
    private float timer;

    private int healAttempt;
    [SerializeField]
    private int oneFourth;

    private int injured;

    public float knockbackTime;
    private float stunKnockback;
    private float userKnockBack;
    private float knockbackCounter;
    

    public Transform Player;
    public float MinDist;
    public float MaxDist;

    private Vector3 playerPos;
    private Quaternion rotate;

    public int rotateSpeed;

    public GameObject monster;

    public int healState;
    public float healCounter;
    private float healTimer;

    [SerializeField]
    private int orbC;

    public float stunLock;
    [SerializeField]
    private float stunTimer;

    public GameObject orbScript;
    private bool wowzer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        //oneFourth = maxHp / 2
        oneFourth = 25;
        injured = 0;
        currentHp = maxHp;
        healState = 0;
        healTimer = 0;
        stunTimer = 0;
        healAttempt = 0;
        stunKnockback = 1.5f;
        userKnockBack = knockbackTime;
        anim.SetInteger("Dead", currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Dead", currentHp);

        orbC = GameObject.FindGameObjectsWithTag("GOrb").Length;
        wowzer = orbScript.GetComponent<AnimFix>().wow;
        
        if(wowzer && healState == 0)
        {
            healState = 1;
        }

        if(injured == 1)
        {

            knockbackCounter += Time.deltaTime;

            if(knockbackCounter > knockbackTime)
            {
                injured = 0;
                knockbackCounter = 0;
            }
        }

        if(healState == 1)
        {
            healTimer += Time.deltaTime;
        }
        
        
        if(healState == 2)
        {
            Debug.Log("Stunned" + Time.deltaTime);
            stunTimer += Time.deltaTime;
        }


        if(healTimer > healCounter && orbC > 0)
        {
            if(orbC == 1)
            {
                currentHp += 10;
            }
            else
            {
                currentHp += 15;
            }
            healState = 4;
            healTimer = 0;
            
        }
        else if(orbC == 0 && healState == 1)
        {
            knockbackTime = stunKnockback;
            anim.SetBool("OHNO",true);
            healState = 2;
            healTimer = 0;
            
        }
        else if(stunTimer > stunLock)
        {
            knockbackTime = userKnockBack;
            anim.SetBool("OHNO",false);
            healState = 4;
            healTimer = 0;
            stunTimer = 0;
            
        }

        
        if (Vector3.Distance(transform.position, Player.position) >= MinDist && Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {
            if(healState < 2 || healState == 4)
            {
             playerPos = Player.position - transform.position;
             playerPos.y = 0;
             rotate = Quaternion.LookRotation(playerPos);
             transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * rotateSpeed);
             timer += Time.deltaTime;
            }

            if(timer > actionCounter)
            {
                if(currentHp < oneFourth && healAttempt != 1)
                {
                    anim.SetTrigger("Summon");
                    healAttempt = 1;
                }
                else
                {
                    anim.SetTrigger("Attack");
                }   
                timer = 0;
            }
        }
        

    }

    public void injure(int damage)
    {
        if(injured == 0)
        {
            if(healState == 2)
            {
                currentHp -= (damage * 2);
            }
            else
            {
                anim.SetBool("Harm",true);
                currentHp -= damage;
            }
            injured = 1;
            timer = 0;
        }

        if(currentHp <= 0)
        {
            healState = 5;
        }
        
    }
}

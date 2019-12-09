using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator anim;

    public GameObject orbs;
    public GameObject orbSpawner;

    public int maxHp;
    private int currentHp;

    public float actionCounter;

    [SerializeField]
    private float timer;

    private int healAttempt;
    [SerializeField]
    private int oneFourth;

    private int injured;

    public float knockbackTime;
    private float knockbackCounter;

    public Transform Player;
    public float MinDist;
    public float MaxDist;

    private Vector3 playerPos;
    private Quaternion rotate;

    public int rotateSpeed;

    public GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        //oneFourth = maxHp - (maxHp / 4);
        oneFourth = 40;
        injured = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(injured == 1)
        {
            knockbackCounter += Time.deltaTime;

            if(knockbackCounter > knockbackTime)
            {
                injured = 0;
                knockbackCounter = 0;
            }
        }

        if (Vector3.Distance(transform.position, Player.position) >= MinDist && Vector3.Distance(transform.position, Player.position) <= MaxDist)
        {
            playerPos = Player.position - transform.position;
            playerPos.y = 0;
            rotate = Quaternion.LookRotation(playerPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * rotateSpeed);


            timer += Time.deltaTime;

            if(timer > actionCounter)
            {

                if(currentHp > oneFourth)
                {
                    anim.SetTrigger("Summon");
                    oneFourth = maxHp - (maxHp / 4);
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
            anim.SetTrigger("Harm");
            injured = 1;
            currentHp -= damage;    
        }
    }
}

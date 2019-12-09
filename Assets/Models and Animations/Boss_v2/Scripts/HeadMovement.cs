using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHp;
    [SerializeField]
    private int currentHP;

    public float knockbackTime;
    private float knockbackCounter;

    private int injured;

    void Start()
    {
        injured = 0;
        currentHP = maxHp;
    }

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
    }

    public void injure(int damage)
    {
        if(injured == 0)
        {
            Debug.Log("please");
            if(damage == 3)
            {
                injured = 1;
                currentHP -= damage;

                if(currentHP == 0)
                {
                    Destroy(this.gameObject);
                }  
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public Animator fullBody;
    
    public GameObject sword;
    public GameObject hammer;
    public GameObject spear;

    public float timer;
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        fullBody.SetBool("RUN", true);
        state = 0;
        timer = 0;
        sword.SetActive(false);
        hammer.SetActive(false);
        spear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3f)
        {
            state = (state + 1)%4;
            timer = 0;
        }

        if(state == 1)
        {
            sword.SetActive(true);
            fullBody.SetBool("Sword", true);
        }
        else if(state == 2)
        {
            fullBody.SetBool("Sword", false);
            sword.SetActive(false);
            hammer.SetActive(true);
            fullBody.SetBool("2h",true);
        }
        else if(state == 3)
        {
            hammer.SetActive(false);
            spear.SetActive(true);
        }
        else
        {
            spear.SetActive(false);
            fullBody.SetBool("2h",false);
        }

    }
}

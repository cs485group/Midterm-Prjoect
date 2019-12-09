using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFix : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;

    public Transform finalP;
    public GameObject Orbs;

    
    public bool wow;
    public float speed;

    void Update()
    {
        if(wow)
        {
            Orbs.transform.position = Vector3.MoveTowards(Orbs.transform.position,finalP.position,Time.deltaTime * speed); 
        }
    }

    public void finishHarm()
    {
        anim.SetBool("Harm", false);
        anim.SetBool("Summon", false);
    }

    public void summoner()
    {
        wow = true;  
    }
}

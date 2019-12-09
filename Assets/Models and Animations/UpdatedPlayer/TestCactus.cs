using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCactus : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageC;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
           collision.gameObject.GetComponent<PlayerController>().HurtPlayer(damageC);
        }
    }
}

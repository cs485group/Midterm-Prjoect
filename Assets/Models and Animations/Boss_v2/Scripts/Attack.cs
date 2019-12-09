using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             Debug.Log("HAH");
            player.GetComponent<PlayerController>().HurtPlayer(2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestListener : MonoBehaviour
{
    //Reference to global Notificatoin Manager
    public NotificationsManager Notifications = null;
    public GameObject obj = null;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        if(Notifications!=null) {
			Notifications.AddListener(this, "OnEnemySlain");
        }

        audio = GetComponent<AudioSource>();
    }

    public void OnEnemySlain(Component Sender)
    {
        //open the chest lid on notification recieved
        obj.transform.Translate(new Vector3(1,0,0.5f));
        obj.transform.Rotate(0,-20,0, Space.Self);
        audio.Play();
    }

}

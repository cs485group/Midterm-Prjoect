using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoster : MonoBehaviour
{
    //Reference to gloabl Notifications Manager
	public NotificationsManager Notifications = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Notifications != null)
            {
                Notifications.PostNotification(this, "OnEnemySlain");
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }
}

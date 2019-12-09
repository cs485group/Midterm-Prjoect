using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ehealthbar : MonoBehaviour
{

    public AudioSource audioClip;
	public NotificationsManager Notifications = null;
    private float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public float SwordDamage;
    public float HammerDamage;
    public GameObject PlayerObject;

    private int injured;
    public float knockbackTime;
    private float knockbackCounter;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        injured = 0;
        knockbackCounter = 0;
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

        slider.value = health;
        if (health < maxHealth)
        {

            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {

            Destroy(gameObject);
            if (Notifications != null)
            {
                Notifications.PostNotification(this, "OnEnemySlain");
            }

        }
    }

    public void injure(int damage)
    {
        if(injured == 0)
        {
            injured = 1;
            health -= damage;    
        }
    }
}

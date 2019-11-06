<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ehealthbar : MonoBehaviour
{

    public AudioSource audioClip;
    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public float SwordDamage;
    public float HammerDamage;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 4;
        health = maxHealth;
        SwordDamage = 0;
        HammerDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
        if (health < maxHealth)
        {

            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            Destroy(gameObject);


        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            SwordDamage = 1;
            health = health = SwordDamage;
        }




        if (other.gameObject.tag == "Hammer")
        {
            HammerDamage = 2;
            health = health - HammerDamage;

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))

        {
            SwordDamage = 1;
            health = health -SwordDamage;
            audioClip.Play();
        }
        if (collision.collider.CompareTag("Hammer"))

        {
            HammerDamage = 2;
            health = health - HammerDamage;
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ehealthbar : MonoBehaviour
{

    public AudioSource audioClip;
    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public float SwordDamage;
    public float HammerDamage;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 4;
        health = maxHealth;
        SwordDamage = 0;
        HammerDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
        if (health < maxHealth)
        {

            healthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            Destroy(gameObject);


        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            SwordDamage = 1;
            health = health = SwordDamage;
        }




        if (other.gameObject.tag == "Hammer")
        {
            HammerDamage = 2;
            health = health - HammerDamage;

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))

        {
            SwordDamage = 1;
            health = health -SwordDamage;
            audioClip.Play();
        }
        if (collision.collider.CompareTag("Hammer"))

        {
            HammerDamage = 2;
            health = health - HammerDamage;
        }
    }
}
>>>>>>> 7aa8d6c01a97ae8836ff0725870ad58804c12366

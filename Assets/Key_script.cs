using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key_script : MonoBehaviour
{
    public GameObject key;
   
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickup();
        NextLevel();
    }
    void pickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.has_key = true;
            Destroy(key);
            
        }
    }
    void NextLevel()
    {
        if (inventory.has_key == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }
    }
}

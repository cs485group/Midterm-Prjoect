using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public Transform blade;

    public float speed;
    public int closeDist;
    public int bladeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * speed * Time.deltaTime;

        if (dist < closeDist)
        {
            blade.Rotate(new Vector3(0,bladeSpeed,0), Space.World);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
        }
    }
}






using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float distance;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        distance += 1 * Time.deltaTime;

        if(distance >= 5)
        {
            Destroy(this.gameObject);
        }
       
    }
}

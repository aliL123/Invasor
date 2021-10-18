using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameObject enemyHit;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        Debug.Log("Checking  : " + other.gameObject.tag);
        if(other.tag == "Enemy")
        {
            enemyHit = other.gameObject;
            enemyHit.GetComponent<EnemyController>().health -= damage;
        }
        
    }
}

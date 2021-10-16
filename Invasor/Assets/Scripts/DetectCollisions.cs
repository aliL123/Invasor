using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checking  : " + other.gameObject.tag);
        if(other.tag == "Bullet")
        {
            animator.SetBool("HasDied", true);
            Destroy(gameObject, 10);
            Destroy(other.gameObject);
        }
        
    }
}

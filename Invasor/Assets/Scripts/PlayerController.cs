using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Animator animator;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public bool canShoot;
    public int delay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            Shoot();
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }
    void playerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
      
        
    }
    void Shoot()
    {
        var instanceBullet = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}

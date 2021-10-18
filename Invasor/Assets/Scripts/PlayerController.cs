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
    public float currentHealth;
    public float maxHealth;
    public GameObject healthBar;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            damageTaken();
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
    void damageTaken()
    {
        currentHealth = currentHealth - 30;
            healthBar.GetComponent<HealthBarController>().UpdateHealthBar();
        }
    void Shoot()
    {
        Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}

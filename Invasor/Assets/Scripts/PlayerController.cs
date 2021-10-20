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
    public float rayDistance;
    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) && canShoot == true)
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
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd,out hit, rayDistance))
        {
            Debug.Log(" Tag : " + hit.collider.tag);
            if(hit.collider.tag != "Town")
            {
                canMove = false;
            }
            
        }
        else
        {
            canMove = true;
        }
        playerMovement();
    
        }

    void playerMovement()
    {
        if(canMove)
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
       // Debug.Log("CantMove!");
      
        
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

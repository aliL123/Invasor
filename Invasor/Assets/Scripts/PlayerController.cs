using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Animator animator;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public bool canShoot;
    public float delay;
    public float currentHealth;
    public float maxHealth;
    public GameObject healthBar;
    public float rayDistance;
    private bool canMove;
    public GameObject panel;
    public GameObject manager;
    private AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
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
            DamageTaken();
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }


    }
    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 back = transform.TransformDirection(Vector3.back);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, rayDistance))
        {
            if (hit.collider.tag != "Town")
            {
                canMove = false;
            }

        }
        else if (Physics.Raycast(transform.position, back, out hit, rayDistance + 2))
        {
            if (hit.collider.tag != "Town")
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
        if (canMove)
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



    }
    public void DamageTaken()
    {
        currentHealth = currentHealth - 60;
        healthBar.GetComponent<HealthBarController>().UpdateHealthBar();
    }
    void Shoot()
    {
        sound.Play();
        Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);

    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    IEnumerator Die()
    {
        manager.GetComponent<ScoreScript>().score = -1; // death score
        panel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // main menu

    }
}

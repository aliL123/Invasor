using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public float health;
    private int maxHealth;
    public Animator animator;
    public GameObject particles;
    [HideInInspector] public bool isDead = false;
    public NavMeshAgent agent;
    private Transform player;
    public GameObject playerObject;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private Rigidbody rb;
    public GameObject manager;
    private bool addedScore = false;
    private void Awake()
    {
        walkPointSet = true;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        this.maxHealth = (int)health;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!isDead)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                
                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {

                Chase();
            }
            if (playerInSightRange && playerInAttackRange)
            {

                Attack();
                Invoke("AttackAction", 5f);
            }
        }

        if (health <= 0)
        {
            if (!addedScore)
            {
                manager.GetComponent<ScoreScript>().score += (maxHealth * 10);
                addedScore = true;
            }

            StartCoroutine(Die());

        }

    }
    private void changeAnim(string state, bool tOrF)
    {
        animator.SetBool(state, tOrF);
    }
    private void Patroling()
    {
        

        if (!walkPointSet)
        {
            Debug.Log("Getting new Destination");
            changeAnim("isChasing", false);
            SearchWalkPoint();
        }
        else
        {
            changeAnim("isLooking", false);
            changeAnim("isChasing", false);
            changeAnim("isAttacking", false);
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Debug.Log(walkPoint);
        if (distanceToWalkPoint.magnitude <= 2f)
        {
            agent.SetDestination(this.transform.position);
            Debug.Log("At Destination");
            
            changeAnim("isLooking", true);
            changeAnim("isChasing", false);
            changeAnim("isAttacking", false);

        }
    }
    private Vector3 SearchWalkPoint()
    {
   
            walkPointSet = true;
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            return walkPoint;
    
            
        
    }

    private void Chase()
    {
        walkPointSet = false;
        changeAnim("isLooking", false);
        changeAnim("isChasing", true);
        changeAnim("isAttacking", false);
        agent.SetDestination(player.position);


    }
    private void Attack()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            changeAnim("isChasing", false);
            changeAnim("isLooking", false);
            Debug.Log("Is attacking soon");
            AttackAction();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void AttackAction()
    {
        changeAnim("isAttacking", true);
        Debug.Log("Animation is done : " + !this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"));
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"))
        {
            player.GetComponent<PlayerController>().damageTaken();
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    IEnumerator Die()
    {

        if (!isDead)
        {
            isDead = true;
            attackRange = 0;
            sightRange = 0;
            this.GetComponent<NavMeshAgent>().enabled = false;
            changeAnim("isAttacking", false);
            changeAnim("isChasing", false);
            changeAnim("isLooking", false);
            changeAnim("HasDied", true);
            particles = Instantiate(particles, transform.position, transform.rotation);

            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(3);
            particles.GetComponent<ParticleSystem>().Stop();
            this.enabled = false;
            Destroy(this.gameObject);


        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}

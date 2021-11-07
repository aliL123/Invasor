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
    private bool isDead = false;
    public NavMeshAgent agent;
    private Transform player;
    public GameObject playerObject;
    public LayerMask whatIsGround, whatIsPlayer;
    //PatrolPoint
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

        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

        this.maxHealth = (int)health;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!isDead)
        {
            if (!playerInSightRange && !playerInAttackRange)  // can't see player
            {
                ChangeAnim("isChasing", false);
                ChangeAnim("isAttacking", false);

                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {
                agent.isStopped = false;
                ChangeAnim("isAttacking", false);

                Chase();
            }
            if (playerInSightRange && playerInAttackRange)
            {
                agent.isStopped = false;
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
    private void ChangeAnim(string state, bool tOrF)
    {
        animator.SetBool(state, tOrF);
    }
    private void Patroling()
    {


        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 0.23f)
        {
            walkPointSet = false;
            agent.isStopped = true;
            ChangeAnim("isLooking", true);


        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {

        ChangeAnim("isLooking", false);
        ChangeAnim("isChasing", true);
        ChangeAnim("isAttacking", false);
        agent.SetDestination(player.position);


    }
    private void Attack()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(player);
        ChangeAnim("isChasing", false);
        if (!alreadyAttacked)
        {
            ChangeAnim("isChasing", false);
            ChangeAnim("isLooking", false);
            AttackAction();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }


    }
    private void AttackAction()
    {
        ChangeAnim("isAttacking", true);

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"))
        {
            player.GetComponent<PlayerController>().DamageTaken();
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

            ChangeAnim("isAttacking", false);
            ChangeAnim("isChasing", false);
            ChangeAnim("isLooking", false);
            ChangeAnim("HasDied", true);

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

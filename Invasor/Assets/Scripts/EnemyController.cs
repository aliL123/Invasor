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
<<<<<<< HEAD
    private bool isDead = false;
=======
    [HideInInspector]public bool isDead = false;
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
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
<<<<<<< HEAD

=======
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
   
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD

=======
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
        this.maxHealth = (int)health;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(!isDead)
        {
            if (!playerInSightRange && !playerInAttackRange)  // can't see player
            {
<<<<<<< HEAD
                ChangeAnim("isChasing", false);
                ChangeAnim("isAttacking", false);

=======
                changeAnim("isChasing", false);
                changeAnim("isAttacking", false);
                Debug.Log("IsPatroling");
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {
<<<<<<< HEAD
                agent.isStopped = false;
                ChangeAnim("isAttacking", false);

=======
                changeAnim("isAttacking", false);
                Debug.Log("IsChasing");
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
                Chase();
            }
            if (playerInSightRange && playerInAttackRange)
            {
<<<<<<< HEAD
                agent.isStopped = false;
=======
               
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
                Attack();
                Invoke("AttackAction", 5f);
            }
        }
<<<<<<< HEAD
=======
        else
        {
           // Debug.Log("Is Dead : " + isDead);
        }
       

>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
        if (health <= 0)
        {
            if(!addedScore)
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

<<<<<<< HEAD

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
=======
       
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
                changeAnim("isLooking", true);
               // Debug.Log("At Destination");
                
            }
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
    }
    private void SearchWalkPoint()
        {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }
    
    private void Chase()
    {
<<<<<<< HEAD

        ChangeAnim("isLooking", false);
        ChangeAnim("isChasing", true);
        ChangeAnim("isAttacking", false);
        agent.SetDestination(player.position);

=======
       
            changeAnim("isLooking", false);
            changeAnim("isChasing", true);
            changeAnim("isAttacking", false);
            agent.SetDestination(player.position);
     
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)

    }
    private void Attack()
    {
<<<<<<< HEAD

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

=======
       
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            changeAnim("isChasing", false);
        if (!alreadyAttacked)
            {
            changeAnim("isChasing", false);
            changeAnim("isLooking", false);
            Debug.Log("Is attacking soon");
                AttackAction();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)

    }
    private void AttackAction()
    {
<<<<<<< HEAD
        ChangeAnim("isAttacking", true);

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"))
=======
        changeAnim("isAttacking", true);
        Debug.Log("Animation is done : " + !this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"));
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"))
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
        {
            player.GetComponent<PlayerController>().DamageTaken();
        }
        
    }
<<<<<<< HEAD


=======
   
>>>>>>> parent of 4ca0706 (Cleaned Up Assets, Added All enemies dead transition)
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

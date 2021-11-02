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
    [HideInInspector]public bool isDead = false;
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
        if(!isDead)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
              //  Debug.Log("IsPatroling");
                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {
              //  Debug.Log("IsChasing");
                Chase();
            }
            if (playerInSightRange && playerInAttackRange)
            {
               
                Attack();
                Invoke("AttackAction", 5f);
            }
        }
        else
        {
           // Debug.Log("Is Dead : " + isDead);
        }
       

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
    private void changeAnim(string state, bool tOrF)
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
                changeAnim("isLooking", false);
                changeAnim("isChasing", false);
                changeAnim("isAttacking", false);
               // Debug.Log("Going to destination");
                agent.SetDestination(walkPoint);
            }
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 0.1f)
            {
                walkPointSet = false;
                changeAnim("isLooking", true);
                changeAnim("isChasing", false);
                changeAnim("isAttacking", false);
               // Debug.Log("At Destination");
                
            }
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

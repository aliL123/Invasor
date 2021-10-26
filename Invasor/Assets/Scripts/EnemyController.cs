using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float health;
    public Animator animator;
    public GameObject particles;
    private bool processing = false;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //PatrolPoint
    public Vector3 walkPoint;
    bool walkPointSet;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(!playerInSightRange && !playerInAttackRange)
        {
            Debug.Log("IsPatroling");
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            Debug.Log("IsChasing");
            Chase();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            Debug.Log("IsAttacking");
            Attack();
        }

        if (health == 0)
        {
           
            StartCoroutine(Die());
            
        }
    }
    private void changeAnim(string state, bool tOrF)
    {
        animator.SetBool(state, tOrF);
    }
    private void Patroling()
    {
        if(!processing)
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
                Debug.Log("Going to destination");
                agent.SetDestination(walkPoint);
            }
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 0.1f)
            {
                walkPointSet = false;
                changeAnim("isLooking", true);
                changeAnim("isChasing", false);
                changeAnim("isAttacking", false);
                Debug.Log("At Destination");
                
            }
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
        if(!processing)
        {
            changeAnim("isLooking", false);
            changeAnim("isChasing", true);
            changeAnim("isAttacking", false);
            agent.SetDestination(player.position);
        }
        
    }
    private void Attack()
    {
        if(!processing)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                changeAnim("isChasing", false);
                changeAnim("isAttacking", true);
                changeAnim("isLooking", false);
                AttackAction();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        
    }
    private void AttackAction()
    {
        changeAnim("isAttacking", true);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    IEnumerator Die()
    {
       
        if (!processing)
        {
            processing = true;
            changeAnim("isAttacking", false);
            changeAnim("isChasing", false);
            changeAnim("isLooking", false);
            changeAnim("HasDied", true);
            particles = Instantiate(particles, transform.position, transform.rotation);
            yield return new WaitForSeconds(2);
            particles.GetComponent<ParticleSystem>().Stop();
            this.enabled = false;
            Destroy(this.gameObject);
            processing = false;
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

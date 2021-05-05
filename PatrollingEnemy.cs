using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public bool isShot;

    float randomTime;
    [Header("Explosion Options")]
    //How far the explosion will reach
    public float explosionRadius = 12.5f;
    //How powerful the explosion is
    public float explosionForce = 4000.0f;

    [Header("Prefabs")]
    //The explosion prefab
    public Transform explosionPrefab;
    //The destroyed barrel prefab
    public Transform destroyedBarrelPrefab;
    //shooting Audio prefab
    public AudioSource shootAudioSource;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    [System.Serializable]
    public class soundClips
    {
        public AudioClip shootSound;
    }
    public soundClips SoundClips;

    private bool soundHasPlayed = false;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        Patrolling();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (isShot)
        {
            health = health - 15;
            isShot = false;
        }

        if (health < 0)
        {

            StartCoroutine(Explode());

        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1f, ForceMode.Impulse);

            shootAudioSource.clip = SoundClips.shootSound;
            shootAudioSource.Play();

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
        
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private IEnumerator Explode()
    {
        //Wait for set amount of time
        yield return new WaitForSeconds(randomTime);
        //Spawn the destroyed barrel prefab
        Instantiate(destroyedBarrelPrefab, transform.position,
                     transform.rotation);

        //Explosion force
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        //Raycast downwards to check the ground tag
        RaycastHit checkGround;
        if (Physics.Raycast(transform.position, Vector3.down, out checkGround, 50))
        {
            //Instantiate explosion prefab at hit position
            Instantiate(explosionPrefab, checkGround.point,
                Quaternion.FromToRotation(Vector3.forward, checkGround.normal));
        }

        //Destroy the current barrel object
        Destroy(gameObject);
    }
}

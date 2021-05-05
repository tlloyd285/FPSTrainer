using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    public int health = 10;
    public int damage = 10;
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

    [Header("Shooting")]
    public float shootingInterval = 2f;
    public float shootingDistance = 30;

    public GameObject player;
    private float shootingTimer;
    private float chasingTimer;
    public float chasingDistance = 50;
    public float chasingInterval = 0f;

    public NavMeshAgent agent;

    public bool isShot;
    private bool routineStarted;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shootingTimer = Random.Range(0, shootingInterval);

        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = true;
        if (isShot)
        {
            health = health - 15;
            isShot = false;
        }
        if (health <= 0)
        {
           
           
                if (routineStarted == false)
                {
                    //Start the explode coroutine
                    StartCoroutine(Explode());
                    routineStarted = true;
                }
            
        }

        //Shooting Logic

        //shooting logic
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
        {
            shootingTimer = shootingInterval;

            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;
        }
        //chasing logic
        chasingTimer -= Time.deltaTime;
        if (chasingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;

            agent.SetDestination(player.transform.position);
        }

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

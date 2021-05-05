using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    
    public float shootingInterval = 2f;
    public float shootingDistance = 10;
    public float chasingDistance = 15;
    public float chasingInterval = 5f;

    public Player player;
    private float shootingTimer;
    private float chasingTimer;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_1").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        shootingTimer = Random.Range(0, shootingInterval);

        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Killed == true)
        {
            agent.enabled = false;
            this.enabled = false;
        }
        //shooting logic
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
        {
            shootingTimer = shootingInterval;

            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;

            //chasing logic
            chasingTimer -= Time.deltaTime;
            if(chasingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance) 
            {
                chasingTimer = chasingInterval;

                agent.SetDestination(player.transform.position);
            }
        }
    }
    protected override void OnKill()
    {
        base.OnKill();

        agent.enabled = false;
        this.enabled = false;
        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 2f;
    public int damage = 10;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } } 

    private float lifeTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Make Bullet Travel
        transform.position += transform.forward * speed * Time.deltaTime;
        //Check if bullet should be destroyed
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}

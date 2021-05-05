using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Gameplay")]
    public int intialHealth = 100;
    

    //public float knockbackForce = 10;
    public float hurtDuration = 0.5f;
    


    private int health;
    public int Health { get { return health; } }

    private bool killed;
    public bool Killed { get { return killed; } }
    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = intialHealth;
       
    }



    //check for collisions
    void OnTriggerEnter(Collider otherCollider)
    {
        
       
        if (isHurt == false)
        {
            GameObject hazard = null;
            if (otherCollider.GetComponent<Enemy>() != null)
            {
            //touching enemy
                /*
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                hazard = enemy.gameObject;
                health -= enemy.damage;
                */

                /*
                //knockback effect
                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce);
                */

 
            } else if (otherCollider.GetComponent<Projectile> () != null)
            {
                Projectile bullet = otherCollider.GetComponent<Projectile>();
                StartCoroutine(HurtRoutine());
                health -= bullet.damage;

            }
            if (hazard != null)
            {
                isHurt = true;
                StartCoroutine(HurtRoutine());
               
            }
            if (health <= 0)
            {
                if (killed == false)
                {
                    killed = true;

                    OnKill();
                }
            }
        }
    }
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        
        isHurt = false;
    }

    private void OnKill()
    {

    }
}

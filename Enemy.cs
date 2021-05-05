using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 5;

    private bool killed = false;
    public bool Killed { get { return killed; } }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<TestBullet> () != null)
        {
            TestBullet bullet = otherCollider.GetComponent<TestBullet>();
            if (bullet.ShotByPlayer == true)
            {
                health -= bullet.damage;
                bullet.gameObject.SetActive(false);

                if (health <= 0)
                {
                    if(killed == false)
                    {
                        killed = true;
                        OnKill();
                    } 
                }
            }
        }
    }


    protected virtual void OnKill() { }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public RectTransform healthbar;
    public int amount;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;



        healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);
    }
}

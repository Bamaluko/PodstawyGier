using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
     * 
     * totalHealth - this enemy's current health.
     * 
     * deathEffect - special effect to be played on enemy's death.
     */

    public int totalHealth = 3;

    public GameObject deathEffect;

    //Damages enemy by selected amount and destroys them, if their health is depleted.
    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            long enemiesKilled = long.Parse(PlayerPrefs.GetString("enemies_killed"));
            enemiesKilled++;
            PlayerPrefs.SetString("enemies_killed", enemiesKilled.ToString());
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}

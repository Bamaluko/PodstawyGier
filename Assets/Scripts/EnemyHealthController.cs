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
    public SpriteRenderer sr;
    private bool isChangingColor = false;

    //Damages enemy by selected amount and destroys them, if their health is depleted.
    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;
        isChangingColor = true;

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

            AudioManager.instance.PlaySFX(4);
        }
    }

    private void Update()
    {
        if (sr.color.a > 0 && isChangingColor) sr.color = new Color(sr.color.r,
                                                  sr.color.g,
                                                  sr.color.b,
                                                  sr.color.a - 12f * Time.deltaTime);
        else if (sr.color.a <= 0)
        {
            sr.color = new Color(sr.color.r,
                                 sr.color.g,
                                 sr.color.b,
                                 1);
            isChangingColor = false;
        }
    }
}

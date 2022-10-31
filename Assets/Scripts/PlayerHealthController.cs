using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
     * 
     * instance - reference to this object.
     * 
     * invincibilityLength - how long will invincibility last.
     * 
     * invincibilityCounter - how long is it lasting already.
     * 
     * flashLength - how long between flashing on and of.
     * 
     * flashCounter - time between flashes that has already passed.
     * 
     * playerSprites - reference to player sprite(s).
     * 
     * currentHealth - players current health.
     * 
     * maxHealth - highest amount of health that player can have.
     */

    public static PlayerHealthController instance;

    public float invincibilityLength;
    private float invincibilityCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            //Dealing with flashing.
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            //If invincibility is over, we make sure to turn on the sprites.
            if (invincibilityCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }

                flashCounter = 0f;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        //All the damage is applied only when we have our invincibility off (we didn't take damage for a little while).
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damageAmount;

            //If the health is equal or below zero, we die and respawn.
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                RespawnController.instance.Respawn();
            }
            //If we are not dead yet, we apply invincibility for a brief moment.
            else
            {
                invincibilityCounter = invincibilityLength;
            }

            //Updating UI after taking damage.
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    //This function fills up the health and calls for UI update.
    public void FillHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    //Heal the player by some amount.
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}

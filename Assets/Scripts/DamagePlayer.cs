using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * destroyOnDamage - should be destroyed after damaging player?
    * 
    * destroyEffect - effect on destroying object.
    * 
    * damageAmount - by how much should the player be damaged.
    */

    public bool destroyOnDamage;

    public GameObject destroyEffect;

    public int damageAmount = 1;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);

        //If this object is to be destroyed on damaging player, then we do it here and instantiate destroy effect.
        if (destroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AcidDamage : MonoBehaviour
{
    public int points;

    public GameObject pickupEffect;

    void OnCollisionEnter2D(Collision2D collision)      // Mozna dodac OnCollisionStay2D i wtedy zadawac damage co okreslony przedzial czasu
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!PlayerPrefs.HasKey("no_damage_water"))
            {
                PlayerHealthController player = collision.gameObject.GetComponentInParent<PlayerHealthController>();

                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }
                player.DamagePlayer(points);
            }
            
        }
    }
}

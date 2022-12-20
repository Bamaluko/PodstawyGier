using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    public int points;

    public GameObject pickupEffect;

    public string healthPickupId;

    private void Start()
    {
        if (PlayerPrefs.HasKey(healthPickupId))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController player = other.GetComponentInParent<PlayerHealthController>();

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            player.MaxHealtIncrease(points);
            PlayerPrefs.SetString(healthPickupId, "pickup collected");
            
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(5);
        }
    }
}


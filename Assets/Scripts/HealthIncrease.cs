using System.Collections;
using System.Collections.Generic;
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
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController player = other.GetComponentInParent<PlayerHealthController>();

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            player.MaxHealtIncrease(points);
            PlayerPrefs.SetString(healthPickupId, "pickup collected");
            //PlayerPrefs.SetInt("max_health", PlayerPrefs.GetInt("max_health") + points);
            Destroy(gameObject);
        }
    }
}


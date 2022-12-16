using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AcidDamage : MonoBehaviour
{
    public int points;

    public string requiredPref;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(requiredPref))
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)      // Mozna dodac OnCollisionStay2D i wtedy zadawac damage co okreslony przedzial czasu
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!PlayerPrefs.HasKey(requiredPref))
            {
                PlayerHealthController.instance.DamagePlayer(points, true);
            }

            if (FindObjectOfType<AcidChase>() != null)
            {
                FindObjectOfType<AcidChase>().enabled = false;
            }
        }
    }
}

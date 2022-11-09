using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnPref : MonoBehaviour
{
    public string prefNoDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !PlayerPrefs.HasKey(prefNoDamage))
        {
            PlayerHealthController.instance.DamagePlayer(99);
        }
    }
}

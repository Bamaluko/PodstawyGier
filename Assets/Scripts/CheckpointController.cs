using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject healEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            RespawnController.instance.setSpawn(transform.position);
            PlayerHealthController.instance.FillHealth();

            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
            PlayerPrefs.SetFloat("PosZ", transform.position.z);

            Instantiate(healEffect, transform.position, transform.rotation);            
            AudioManager.instance.PlaySFX(5);
        }
    }
}

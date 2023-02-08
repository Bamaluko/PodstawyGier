using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointController : MonoBehaviour
{
    public GameObject healEffect;
    public TMP_Text info;
    public float infoShowTime;
    private bool showInfo = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(info != null)
            {
                info.gameObject.SetActive(true);
                showInfo = true;
            }
            RespawnController.instance.setSpawn(transform.position);
            PlayerHealthController.instance.FillHealth();

            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
            PlayerPrefs.SetFloat("PosZ", transform.position.z);

            Instantiate(healEffect, transform.position, transform.rotation);            
            AudioManager.instance.PlaySFX(5);
        }
    }

    public void Update()
    {
        if (showInfo && infoShowTime >= 0) infoShowTime -= Time.deltaTime;
        else if(showInfo)
        {
            showInfo = false;
            info.gameObject.SetActive(false);
        }
    }
}

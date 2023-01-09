using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnCollision : MonoBehaviour
{
    public String requiredPref;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.HasKey(requiredPref))
        {
            AudioManager.instance.PlayElevatorMusic();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayLevelMusic();
            Destroy(gameObject);
        }
    }
}

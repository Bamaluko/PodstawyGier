using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerPrefs.DeleteAll();
            Destroy(PlayerHealthController.instance.gameObject);
            Destroy(UIController.instance.gameObject);
            Destroy(RespawnController.instance.gameObject);
            SceneManager.LoadScene("Main Menu");
        }
    }
}

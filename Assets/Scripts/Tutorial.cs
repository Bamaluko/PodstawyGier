using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject screen;
    public GameObject trigerScreen;
    public GameObject vspotScreen;
    //public BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("tutorial1") == 1 && SceneManager.GetActiveScene().name == "Tutorial1") {
            screen.SetActive(false);
        }
        if (PlayerPrefs.GetInt("tutorial2") == 1 && SceneManager.GetActiveScene().name == "Tutorial4")
        {
            trigerScreen.SetActive(false);
        }
        if (PlayerPrefs.GetInt("tutorial3") == 1 && SceneManager.GetActiveScene().name == "Room2")
        { 
            vspotScreen.SetActive(false);
            enabled = false;
        }
    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial1") {
            PlayerPrefs.SetInt("tutorial1", 1);
            screen.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Room2") {
            PlayerPrefs.SetInt("tutorial3", 1);
            vspotScreen.SetActive(false);
        }
        Time.timeScale = 1.0f;
        FindObjectOfType<PlayerController>().canMove = true;
        FindObjectOfType<CameraShaker>().enabled = true;
    }

    public void ContinueTrigger()
    {
        PlayerPrefs.SetInt("tutorial2", 1);
        trigerScreen.SetActive(false);
        Time.timeScale = 1.0f;
        FindObjectOfType<PlayerController>().canMove = true;
        FindObjectOfType<CameraShaker>().enabled = true;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerPrefs.HasKey("tutorial2"))
        {   
            trigerScreen.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<PlayerController>().canMove = false;
            FindObjectOfType<CameraShaker>().enabled = false;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Room2" && vspotScreen.activeSelf == false)
        {
            vspotScreen.SetActive(true);
            enabled = false;
        }
        if (screen.activeSelf || vspotScreen.activeSelf)
        {
            Time.timeScale = 0;
            FindObjectOfType<PlayerController>().canMove = false;
            FindObjectOfType<CameraShaker>().enabled = false;
        }
    }
}

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
        if (PlayerPrefs.GetInt("tutorial1") == 1 && SceneManager.GetActiveScene().name == "Room1") {
            Destroy(screen);
        }
        if (PlayerPrefs.GetInt("tutorial2") == 1 && SceneManager.GetActiveScene().name == "Room1")
        {
            Destroy(trigerScreen);
        }
        if (PlayerPrefs.GetInt("tutorial3") == 1 && SceneManager.GetActiveScene().name == "Room2")
        { 
            Destroy(vspotScreen);
            enabled = false;
        }
    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().name == "Room1") {
            PlayerPrefs.SetInt("tutorial1", 1);
            Destroy(screen);
        }
        else if (SceneManager.GetActiveScene().name == "Room2") {
            PlayerPrefs.SetInt("tutorial3", 1);
            Destroy(vspotScreen);
        }
        Time.timeScale = 1.0f;
        
    }

    public void ContinueTrigger()
    {
        PlayerPrefs.SetInt("tutorial2", 1);
        Destroy(trigerScreen);
        Time.timeScale = 1.0f;
        FindObjectOfType<PlayerController>().canMove = true;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerPrefs.HasKey("tutorial2"))
        {   
            trigerScreen.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<PlayerController>().canMove = false;

        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Room2" && vspotScreen.activeSelf == false)
        {
            vspotScreen.SetActive(true);
            enabled = false;
        }
    }
}

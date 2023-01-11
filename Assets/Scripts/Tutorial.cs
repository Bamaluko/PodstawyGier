using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject screen;
    public GameObject trigerScreen;
    //public BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("tutorial1") == 1) {
            Destroy(screen);
        }
        if (PlayerPrefs.GetInt("tutorial2") == 1)
        {
            Destroy(trigerScreen);
        }
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("tutorial1", 1);
        Destroy(screen);
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
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("tutorial2") != 1)
        {   
            trigerScreen.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<PlayerController>().canMove = false;

        }
        
    }

}

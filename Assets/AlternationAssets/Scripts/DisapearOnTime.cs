using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using UnityEngine.UIElements;

public class DisapearOnTime : MonoBehaviour
{
    public string prefRequired;
    public bool isOn;
    public float timeToTogglePlatform;

    private float currentTime = 0;
    private bool isColide = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired) || isOn)
        {
            if (isColide)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToTogglePlatform)
                {
                    currentTime = 0;
                    TogglePlatform(!isColide);
                    GetComponent<Collider2D>().enabled = !isColide;
                    isColide = false;
                }
            }
            else if (!isColide)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToTogglePlatform)
                {
                    currentTime = 0;
                    TogglePlatform(!isColide);
                    GetComponent<Collider2D>().enabled = !isColide;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColide = true;
    }

    void TogglePlatform(bool active)
    {
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

}   


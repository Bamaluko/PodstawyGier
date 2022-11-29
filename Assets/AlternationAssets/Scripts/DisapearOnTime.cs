using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DisapearOnTime : MonoBehaviour
{
    public string prefRequired;
    public bool debugIsOn;
    public float timeToTogglePlatform;

    private float currentTime = 0;
    private bool isColide = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired) || debugIsOn)
        {
            if (isColide)
            {
                gameObject.GetComponent<Tilemap>().color = 
                    new Color(gameObject.GetComponent<Tilemap>().color.r, 
                        gameObject.GetComponent<Tilemap>().color.g, 
                        gameObject.GetComponent<Tilemap>().color.b,
                        1 - currentTime/timeToTogglePlatform);
                
                currentTime += Time.deltaTime;
                if (currentTime >= timeToTogglePlatform)
                {
                    currentTime = timeToTogglePlatform;
                    GetComponent<TilemapCollider2D>().enabled = !isColide;
                    isColide = false;
                }
            }
            else if (!isColide && currentTime > 0)
            {
                currentTime -= 2 * Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    gameObject.GetComponent<Tilemap>().color = 
                        new Color(gameObject.GetComponent<Tilemap>().color.r, 
                            gameObject.GetComponent<Tilemap>().color.g, 
                            gameObject.GetComponent<Tilemap>().color.b,
                            1 );
                    GetComponent<TilemapCollider2D>().enabled = !isColide;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColide = true;
        }
    }
}   


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnPref : MonoBehaviour
{
    
    public string prefRequired;
    public bool debugIsOn;
    public float fallSpeed;

    private float initialSpot;
    private bool isFalling = false;
    private float timer = 0;

    private void Start()
    {
        initialSpot = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired) || debugIsOn)
        {
            if (!isFalling && initialSpot > transform.position.y)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (fallSpeed / 2) * Time.deltaTime,
                        transform.position.z);
                }
            }
            else if(isFalling)
            {
                transform.position =
                    new Vector3(transform.position.x, transform.position.y - fallSpeed  * Time.deltaTime, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGroundSpot"))
        {
            isFalling = true;
            timer = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isFalling = false;
    }
}

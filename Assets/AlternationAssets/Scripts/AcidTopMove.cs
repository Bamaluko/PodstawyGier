using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTopMove : MonoBehaviour
{

    public float moveTime = 1;

    private float counter = 0;

    private Vector3 oryginalSpot;
    // Update is called once per frame

    private void Start()
    {
        oryginalSpot = transform.position;
        if (PlayerPrefs.HasKey("freeze_acid"))
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (UIController.instance != null)
        {
            if (counter < moveTime && !UIController.instance.pauseScreen.gameObject.activeSelf)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z);
                counter += Time.deltaTime;
            }
            else if (counter < 2 * moveTime && !UIController.instance.pauseScreen.gameObject.activeSelf && transform.position.y < oryginalSpot.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.001f, transform.position.z);
                counter += Time.deltaTime;
            }
            else if(!UIController.instance.pauseScreen.gameObject.activeSelf)
            {
                counter = 0;
                transform.position = oryginalSpot;
            }
        }
    }
}

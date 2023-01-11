using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidChase : MonoBehaviour
{
    public GameObject acid;
    public TilemapDestructor destructor;
    
    public float speed;
    public float maxHeight;
    private bool isChasing;
    private PlayerController player;

    private void Start()
    {
        enabled = false;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= maxHeight)
        {
            enabled = false;
        }

        if (player.transform.position.y - transform.position.y > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * 3 * Time.deltaTime), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        if (col.CompareTag("Player") && !isChasing)
        {
            if (!enabled)
            {
                player = FindObjectOfType<PlayerController>();
                StartCoroutine(FindObjectOfType<CameraShaker>().Shake(3, 1));
                transform.position = new Vector3(transform.position.x, player.transform.position.y + 5, transform.position.z);
                enabled = true;
                destructor.Destruction();
                isChasing = true;

                AudioManager.instance.PlayBossMusic();
                AudioManager.instance.PlaySFX(8);
            }
        }
    }
}

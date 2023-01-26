using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTeleport : MonoBehaviour
{
    public WindTeleport sibling;

    public GameObject teleportEffect;

    private bool teleportOnTouch = true;


    private void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(teleportEffect, new Vector3(sibling.transform.position.x, 
            sibling.transform.position.y, sibling.transform.position.z), Quaternion.identity);
        if (col.gameObject.CompareTag("Player") && teleportOnTouch)
        {
            sibling.teleportOnTouch = false;
            PlayerHealthController.instance.gameObject.transform.position = new Vector3(sibling.transform.position.x, 
                sibling.transform.position.y + 1, sibling.transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teleportOnTouch = true;
        }
    }
}
